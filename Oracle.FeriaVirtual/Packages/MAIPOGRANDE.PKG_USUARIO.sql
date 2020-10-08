-- ****** Objeto: Paquete MAIPOGRANDE.PKG_USUARIO Fecha de Script: 01-10-2020 14:58:54 ******
CREATE OR REPLACE PACKAGE "MAIPOGRANDE"."PKG_USUARIO" AS
PROCEDURE SP_LoginUsuario(p_RutUsuario IN VARCHAR2, p_ClaveUsuario IN VARCHAR2,  p_IdPerfil IN INTEGER, 
                          p_IdUsuario OUT INTEGER, p_glosa OUT VARCHAR2,p_estado OUT INTEGER);
PROCEDURE SP_Reintentos(p_RutUsuario IN VARCHAR2, p_IdPerfil IN INTEGER, 
                       p_Son OUT INTEGER, p_Activo OUT CHAR,
                       p_glosa OUT VARCHAR2,p_estado OUT INTEGER);
PROCEDURE SP_GeneraSesion(p_idUsuario IN INTEGER, p_Fecha IN varchar2,
                          p_Guid IN VARCHAR2, p_Json IN Varchar2,
                          p_glosa OUT VARCHAR2,p_estado OUT INTEGER);                          
END "PKG_USUARIO";
