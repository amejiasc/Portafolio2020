-- ****** Objeto: Paquete MAIPOGRANDE.PKG_CONFIGURACION Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE "MAIPOGRANDE"."PKG_CONFIGURACION" AS

PROCEDURE SP_Crear_Configuracion(p_Nombrepaso IN VARCHAR2, p_TipoPaso IN VARCHAR2, p_IdAnterior in number,p_IdPosterior IN Number,p_IdConfiguracion out number, p_estado OUT number, p_glosa OUT VARCHAR2);
PROCEDURE SP_Listar_Configuracion(p_IdConfiguracion IN NUMBER,p_estado out number, p_glosa out VARCHAR2,OUT_PC_GET_Configuracion OUT SYS_REFCURSOR);
PROCEDURE SP_Modificar_Configuracion(p_IdConfiguracion IN number, p_Nombrepaso IN VARCHAR2, p_TipoPaso IN VARCHAR2, p_IdAnterior in number,p_IdPosterior IN Number,p_estado OUT number, p_glosa OUT VARCHAR2);
PROCEDURE SP_Eliminar_Configuracion(p_IdConfiguracion IN INTEGER,p_estado OUT number, p_glosa OUT VARCHAR2);
end "PKG_CONFIGURACION";
