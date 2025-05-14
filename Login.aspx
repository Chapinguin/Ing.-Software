<%@ Page Title="Login" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="Login.aspx.vb" Inherits="CalculadoraDeDesgaste.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .login-container {
            text-align: center;
            margin-top: 100px;
        }
        .btn-login {
            background-color: #28a745;
            color: white;
            padding: 10px 30px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div class="login-container">
        <h2>Iniciar Sesión</h2>
        
        <asp:TextBox ID="txtCorreoLogin" runat="server" placeholder="Correo electrónico"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCorreoLogin" 
            ErrorMessage="Campo requerido" ForeColor="Red"/>
        
        <br/>
        
        <asp:TextBox ID="txtContrasenaLogin" runat="server" TextMode="Password" 
            placeholder="Contraseña"/>
        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtContrasenaLogin" 
            ErrorMessage="Campo requerido" ForeColor="Red"/>
        
        <br/><br/>
        
        <asp:Button ID="btnLogin" runat="server" Text="Ingresar" 
            CssClass="btn-login" OnClick="btnLogin_Click"/>
        
        <br/><br/>
        <asp:Label ID="lblMensajeLogin" runat="server" CssClass="error-msg"/>
    </div>
       <asp:Button ID="Button1" runat="server" Text="Entrar" CssClass="btn-login" OnClick="btnLogin_Click" />
    
    <br /><br />
    
    <%-- Enlace para registrarse --%>
    <div style="margin-top: 15px;">
        <span>¿No tienes cuenta? </span>
        <asp:HyperLink 
            ID="lnkRegistro" 
            runat="server" 
            NavigateUrl="~/Registro.aspx" 
            CssClass="link-registro">
            Regístrate aquí
        </asp:HyperLink>
    </div>

    <asp:Label ID="lblMensaje" runat="server" CssClass="error-msg" />
</div>
</asp:Content>