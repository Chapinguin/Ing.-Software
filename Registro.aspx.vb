Imports System.Data.SqlClient
Imports System.Security.Cryptography ' Para el hashing de contraseñas
Imports System.Text ' Para el hashing de contraseñas
Imports System.Configuration ' Para leer la cadena de conexión del Web.config

Namespace CalculadoraDeDesgaste

    Public Class Registro
        Inherits System.Web.UI.Page

        ' Declarar la instancia de UsuarioDB a nivel de clase
        Private db As UsuarioDB
        ' Declarar la cadena de conexión a nivel de clase (opcional aquí si UsuarioDB la maneja internamente)
        Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("ConexionCalculadora (CalculadoraDeDesgaste)").ConnectionString

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            ' Inicializar la instancia de UsuarioDB al cargar la página
            ' Puedes pasarle la cadena de conexión o dejar que UsuarioDB la tome del Web.config
            ' si tiene un constructor sin parámetros que haga eso.
            ' Usando el constructor que espera la cadena de conexión:
            db = New UsuarioDB(connString)
            ' O si tu UsuarioDB tiene un constructor por defecto que lee del config:
            ' db = New UsuarioDB()

            If Not IsPostBack Then
                ' Código que solo quieres que se ejecute la primera vez que se carga la página
                lblMensajeRegistro.Visible = False ' Asegurarse que el mensaje esté oculto inicialmente
            End If
        End Sub

        Protected Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
            ' Validar los controles del lado del servidor primero
            If Page.IsValid Then
                ' Usar los IDs CORRECTOS de los TextBoxes
                Dim correo As String = txtCorreoRegistro.Text.Trim()
                Dim contrasena As String = txtContrasenaRegistro.Text ' No hacer Trim() a la contraseña
                ' Dim confirmarContrasena As String = txtConfirmarContrasena.Text ' Ya validado por CompareValidator

                Try
                    If db.CorreoExiste(correo) Then
                        MostrarMensaje("El correo electrónico ya está registrado.", esError:=True)
                        Return
                    End If

                    ' Hashear la contraseña ANTES de guardarla
                    Dim hashContrasena As String = HashPassword(contrasena)

                    ' Registrar el usuario con el CORREO y el HASH de la contraseña
                    If db.RegistrarUsuario(correo, hashContrasena) Then
                        ' MostrarMensaje("¡Registro exitoso! Ahora puedes iniciar sesión.", esError:=False)
                        ' Limpiar campos después de un registro exitoso
                        txtCorreoRegistro.Text = String.Empty
                        txtContrasenaRegistro.Text = String.Empty
                        txtConfirmarContrasena.Text = String.Empty
                        ' Redirigir a Login con un mensaje de éxito (opcional)
                        Response.Redirect("Login.aspx?registro=exito", False)
                        Context.ApplicationInstance.CompleteRequest() ' Necesario después de Response.Redirect en algunos casos
                    Else
                        MostrarMensaje("Ocurrió un error durante el registro. Por favor, inténtalo de nuevo.", esError:=True)
                    End If
                Catch ex As Exception
                    MostrarMensaje("Error del sistema durante el registro: " & ex.Message, esError:=True)
                    ' Para depuración, puedes ver el error completo en la consola de depuración de VS
                    System.Diagnostics.Debug.WriteLine("Error en btnRegistrar_Click: " & ex.ToString())
                End Try
            Else
                ' Los validadores ASP.NET mostrarán sus propios mensajes
                ' Puedes añadir un mensaje general aquí si quieres, usando MostrarMensaje
            End If
        End Sub

        ' Función para hashear la contraseña (debe ser la misma que usarás en el Login)
        Private Function HashPassword(password As String) As String
            Using sha256 As SHA256 = SHA256.Create()
                Dim bytes As Byte() = Encoding.UTF8.GetBytes(password)
                Dim hashBytes As Byte() = sha256.ComputeHash(bytes)
                Dim builder As New StringBuilder()
                For i As Integer = 0 To hashBytes.Length - 1
                    builder.Append(hashBytes(i).ToString("x2"))
                Next
                Return builder.ToString()
            End Using
        End Function

        ' Método helper para mostrar mensajes al usuario
        Private Sub MostrarMensaje(mensaje As String, esError As Boolean)
            lblMensajeRegistro.Text = mensaje ' Usar el ID CORRECTO del Label
            If esError Then
                lblMensajeRegistro.CssClass = "mensaje mensaje-error"
            Else
                lblMensajeRegistro.CssClass = "mensaje mensaje-exito"
            End If
            lblMensajeRegistro.Visible = True
        End Sub

    End Class
End Namespace