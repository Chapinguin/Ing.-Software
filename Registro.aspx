<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="Registro.aspx.vb" Inherits="CalculadoraDeDesgaste.Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Usuario</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; background-color: #f4f4f4; }
        .container { background-color: #fff; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0,0,0,0.1); max-width: 400px; margin: auto; }
        h2 { text-align: center; color: #333; }
        .form-group { margin-bottom: 15px; }
        .form-group label { display: block; margin-bottom: 5px; font-weight: bold; }
        .form-group .form-control { width: calc(100% - 22px); padding: 10px; border: 1px solid #ddd; border-radius: 4px; }
        .btn-registro { background-color: #5cb85c; color: white; padding: 10px 15px; border: none; border-radius: 4px; cursor: pointer; font-size: 16px; width: 100%; }
        .btn-registro:hover { background-color: #4cae4c; }
        .mensaje { margin-top: 15px; padding: 10px; border-radius: 4px; text-align: center; }
        .mensaje-exito { background-color: #dff0d8; color: #3c763d; border: 1px solid #d6e9c6; }
        .mensaje-error { background-color: #f2dede; color: #a94442; border: 1px solid #ebccd1; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>Registro de Nuevo Usuario</h2>

            <div class="form-group">
                <label for="txtCorreoRegistro">Correo Electrónico:</label>
                <asp:TextBox ID="txtCorreoRegistro" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCorreo" runat="server"
                    ControlToValidate="txtCorreoRegistro"
                    ErrorMessage="El correo es obligatorio."
                    Display="Dynamic" ForeColor="Red" ValidationGroup="RegistroGroup"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revCorreo" runat="server"
                    ControlToValidate="txtCorreoRegistro"
                    ErrorMessage="Formato de correo inválido."
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    Display="Dynamic" ForeColor="Red" ValidationGroup="RegistroGroup"></asp:RegularExpressionValidator>
            </div>

            <div class="form-group">
                <label for="txtContrasenaRegistro">Contraseña:</label>
                <asp:TextBox ID="txtContrasenaRegistro" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvContrasena" runat="server"
                    ControlToValidate="txtContrasenaRegistro"
                    ErrorMessage="La contraseña es obligatoria."
                    Display="Dynamic" ForeColor="Red" ValidationGroup="RegistroGroup"></asp:RequiredFieldValidator>
            </div>

            <div class="form-group">
                <label for="txtConfirmarContrasena">Confirmar Contraseña:</label>
                <asp:TextBox ID="txtConfirmarContrasena" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConfirmarContrasena" runat="server"
                    ControlToValidate="txtConfirmarContrasena"
                    ErrorMessage="Confirmar la contraseña es obligatorio."
                    Display="Dynamic" ForeColor="Red" ValidationGroup="RegistroGroup"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvContrasena" runat="server"
                    ControlToValidate="txtConfirmarContrasena"
                    ControlToCompare="txtContrasenaRegistro"
                    Operator="Equal"
                    ErrorMessage="Las contraseñas no coinciden."
                    Display="Dynamic" ForeColor="Red" ValidationGroup="RegistroGroup"></asp:CompareValidator>
            </div>
            
            <asp:Button ID="btnRegistrar" runat="server" Text="Registrarse" 
                CssClass="btn-registro" ValidationGroup="RegistroGroup"/>
            <%-- El OnClick se maneja con Handles en el code-behind --%>

            <asp:Label ID="lblMensajeRegistro" runat="server" CssClass="mensaje" Visible="false"></asp:Label>

        </div>
    </form>
</body>
</html>