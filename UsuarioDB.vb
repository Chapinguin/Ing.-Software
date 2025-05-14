Imports System.Data.SqlClient
Imports System.Configuration ' Asegúrate que esta importación esté

Public Class UsuarioDB
    Private ReadOnly _connString As String ' Cambiado a ReadOnly y prefijo _

    ' Constructor que acepta la cadena de conexión
    Public Sub New(connectionString As String)
        If String.IsNullOrWhiteSpace(connectionString) Then
            Throw New ArgumentNullException("connectionString", "La cadena de conexión no puede estar vacía.")
        End If
        Me._connString = connectionString
    End Sub

    ' Constructor sin argumentos (si quieres mantener la compatibilidad con el uso anterior)
    ' Podría leer del Web.config aquí directamente si se llama a este constructor.
    Public Sub New()
        Me._connString = ConfigurationManager.ConnectionStrings("ConexionCalculadora (CalculadoraDeDesgaste)").ConnectionString
        If String.IsNullOrWhiteSpace(Me._connString) Then
            Throw New InvalidOperationException("No se encontró la cadena de conexión 'ConexionCalculadora (CalculadoraDeDesgaste)' en el archivo de configuración.")
        End If
    End Sub


    Public Function CorreoExiste(correo As String) As Boolean
        Using conn As New SqlConnection(_connString) ' Usar _connString
            conn.Open()
            Using cmd As New SqlCommand("SELECT COUNT(*) FROM Usuarios WHERE Correo = @Correo", conn)
                cmd.Parameters.AddWithValue("@Correo", correo)
                Return CInt(cmd.ExecuteScalar()) > 0
            End Using
        End Using
    End Function

    Public Function RegistrarUsuario(correo As String, hashContrasena As String) As Boolean
        Try
            Using conn As New SqlConnection(_connString) ' Usar _connString
                conn.Open()
                Using cmd As New SqlCommand(
                    "INSERT INTO Usuarios (Correo, Contraseña, FechaRegistro) 
                    VALUES (@Correo, @Contrasena, GETDATE())", conn)

                    cmd.Parameters.AddWithValue("@Correo", correo)
                    cmd.Parameters.AddWithValue("@Contrasena", hashContrasena)
                    Return cmd.ExecuteNonQuery() > 0
                End Using
            End Using
        Catch ex As SqlException
            ' Loguear el error (ex.ToString()) o manejarlo específicamente
            System.Diagnostics.Debug.WriteLine("Error SQL en RegistrarUsuario: " & ex.Message)
            Return False ' Indicar fallo
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Error general en RegistrarUsuario: " & ex.Message)
            Return False ' Indicar fallo
        End Try
    End Function

    Public Function ObtenerHashContrasena(correo As String) As String
        Using conn As New SqlConnection(_connString) ' Usar _connString
            conn.Open()
            Using cmd As New SqlCommand("SELECT Contraseña FROM Usuarios WHERE Correo = @Correo", conn)
                cmd.Parameters.AddWithValue("@Correo", correo)
                Dim result = cmd.ExecuteScalar()
                Return If(result IsNot DBNull.Value AndAlso result IsNot Nothing, result.ToString(), Nothing) ' Devolver Nothing si no se encuentra
            End Using
        End Using
    End Function
End Class