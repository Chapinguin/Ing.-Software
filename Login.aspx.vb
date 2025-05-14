Imports System.Web.Security ' Para FormsAuthentication
Imports System.Security.Cryptography ' Para el hashing
Imports System.Text ' Para el hashing
Imports System.Configuration ' Para leer la cadena de conexión

Namespace CalculadoraDeDesgaste ' Asegúrate que coincida

    Public Class Login
        Inherits System.Web.UI.Page

        ' Declarar la instancia de UsuarioDB a nivel de clase
        Private db As UsuarioDB
        ' Declarar la cadena de conexión a nivel de clase
        Private ReadOnly connString As String = ConfigurationManager.ConnectionStrings("ConexionCalculadora (CalculadoraDeDesgaste)").ConnectionString

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            ' Inicializar la instancia de UsuarioDB al cargar la página
            db = New UsuarioDB(connString) ' Usar el constructor que toma la cadena

            If Not IsPostBack Then
                lblMensajeLogin.Visible = False ' Ocultar mensaje al inicio
                ' Verificar si viene de un registro exitoso
                If Request.QueryString("registro") = "exito" Then
                    lblMensajeLogin.Text = "¡Registro exitoso! Por favor, inicia sesión."
                    lblMensajeLogin.ForeColor = System.Drawing.Color.Green ' O usa una clase CSS
                    lblMensajeLogin.Visible = True
                End If
            End If
        End Sub

        Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
            ' Asegúrate de tener validadores para txtCorreoLogin y txtContrasenaLogin en tu Login.aspx
            ' y que pertenezcan a un ValidationGroup si es necesario, y que btnLogin valide ese grupo.
            ' Por simplicidad, asumiré que Page.IsValid funciona como esperas.
            Page.Validate() ' Llama explícitamente a la validación
            If Page.IsValid Then
                Dim correo As String = txtCorreoLogin.Text.Trim()
                Dim contrasenaIngresada As String = txtContrasenaLogin.Text

                Try
                    ' db ya debería estar inicializado desde Page_Load
                    If db Is Nothing Then
                        ' Esto no debería pasar si Page_Load se ejecutó correctamente
                        lblMensajeLogin.Text = "Error crítico: La conexión a la base de datos no está inicializada."
                        lblMensajeLogin.Visible = True
                        Return
                    End If

                    ' 1. Obtener el HASH almacenado para el correo ingresado
                    '    Asegúrate que el método se llame ObtenerHashContrasena en UsuarioDB.vb
                    Dim hashAlmacenado As String = db.ObtenerHashContrasena(correo)

                    If Not String.IsNullOrEmpty(hashAlmacenado) Then
                        ' 2. Hashear la contraseña que el usuario acaba de ingresar
                        Dim hashIngresado As String = HashPasswordLogin(contrasenaIngresada)

                        ' 3. Comparar los hashes
                        If hashAlmacenado.Equals(hashIngresado, StringComparison.OrdinalIgnoreCase) Then
                            ' Autenticación exitosa
                            FormsAuthentication.RedirectFromLoginPage(correo, False) ' False = no crear cookie persistente
                        Else
                            ' Contraseña incorrecta
                            lblMensajeLogin.Text = "Correo o contraseña incorrectos."
                            lblMensajeLogin.Visible = True
                        End If
                    Else
                        ' Correo no encontrado
                        lblMensajeLogin.Text = "Correo o contraseña incorrectos."
                        lblMensajeLogin.Visible = True
                    End If
                Catch ex As Exception
                    lblMensajeLogin.Text = "Error del sistema al intentar iniciar sesión."
                    lblMensajeLogin.Visible = True
                    System.Diagnostics.Debug.WriteLine("Error en btnLogin_Click (Login.aspx.vb): " & ex.ToString())
                End Try
            Else
                lblMensajeLogin.Text = "Por favor, ingrese correo y contraseña." ' Mensaje genérico si la validación falla
                lblMensajeLogin.Visible = True
            End If
        End Sub

        ' Esta función DEBE ser idéntica a la función HashPassword en Registro.aspx.vb
        ' Idealmente, esta función debería estar en una clase de Utilidades compartida o en UsuarioDB.
        Private Function HashPasswordLogin(password As String) As String
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

    End Class
End Namespace