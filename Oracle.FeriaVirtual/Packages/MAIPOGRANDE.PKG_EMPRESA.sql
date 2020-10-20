-- ****** Objeto: Paquete MAIPOGRANDE.PKG_EMPRESA Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE "MAIPOGRANDE"."PKG_EMPRESA" AS
PROCEDURE SP_Crear_Empresa(p_RutEmpresa IN VARCHAR2, p_NombreEmpresa IN VARCHAR2, p_Direccion IN VARCHAR2, p_Region in number,p_Ciudad IN VARCHAR2, p_Email IN VARCHAR2,p_Telefono in number,p_Celular in number,p_IdComuna in number,p_estado out number, p_glosa out VARCHAR2);
PROCEDURE SP_Listar_Empresa(p_RutEmpresa IN VARCHAR2,OUT_PC_GET_Empresa OUT SYS_REFCURSOR,p_estado out number, p_glosa out VARCHAR2);
PROCEDURE SP_Modificar_Empresa(p_RutEmpresa IN VARCHAR2, p_NombreEmpresa IN VARCHAR2, p_Direccion IN VARCHAR2, p_Region in number,p_Ciudad IN VARCHAR2, p_Email IN VARCHAR2,p_Telefono in number,p_Celular in number,p_IdComuna in number,p_estado out number, p_glosa out VARCHAR2);
PROCEDURE SP_Eliminar_Empresa(p_RutEmpresa IN VARCHAR2,p_estado out number, p_glosa out VARCHAR2);

end "PKG_EMPRESA";
