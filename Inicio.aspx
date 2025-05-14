<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master.Master" CodeBehind="Inicio.aspx.vb" Inherits="CalculadoraDeDesgaste.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .inicio-container {
            text-align: center;
            margin-top: 100px;
        }

        .inicio-container h1 {
            font-family: Arial;
            color: #333;
            margin-bottom: 40px;
        }

        .boton-accion {
            margin: 10px;
            padding: 12px 30px;
            background-color: #007bff;
            color: white;
            border: none;
            font-size: 16px;
            border-radius: 5px;
            cursor: pointer;
        }

        .boton-accion:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div class="inicio-container">

        <asp:Button ID="btnIniciarSesion" runat="server" Text="Iniciar Sesión"
            CssClass="boton-accion" PostBackUrl="~/Login.aspx" />

        <asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse"
            CssClass="boton-accion" PostBackUrl="~/Registro.aspx" />
    </div>
</asp:Content>
