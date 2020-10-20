-- ****** Objeto: Paquete MAIPOGRANDE.PKG_COMUNA Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE "MAIPOGRANDE"."PKG_COMUNA" AS
PROCEDURE SP_Crear_Comuna(p_IdComuna IN INTEGER, p_CodigoComuna IN INTEGER, p_IdRegion IN INTEGER, p_NombreComuna in VARCHAR2,p_NombreCiudad IN VARCHAR2, p_NombreRegion IN VARCHAR2,p_estado OUT INTEGER, p_glosa OUT VARCHAR2);
PROCEDURE SP_Listar_Comuna(p_idComuna IN NUMBER,OUT_PC_GET_Comuna OUT SYS_REFCURSOR,p_estado out number, p_glosa out VARCHAR2);
PROCEDURE SP_Modificar_Comuna(p_IdComuna IN number, p_CodigoComuna IN number, p_IdRegion IN number, p_NombreComuna in VARCHAR2,p_NombreCiudad IN VARCHAR2, p_NombreRegion IN VARCHAR2,p_estado OUT number, p_glosa OUT VARCHAR2);
PROCEDURE SP_Eliminar_Comuna(p_IdComuna IN number,p_estado OUT number, p_glosa OUT VARCHAR2);

end "PKG_COMUNA";
