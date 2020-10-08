-- ****** Objeto: Cuerpo del Paquete MAIPOGRANDE.PKG_USUARIO.BODY Fecha de Script: 01-10-2020 14:58:54 ******
CREATE OR REPLACE PACKAGE BODY "MAIPOGRANDE"."PKG_USUARIO" AS
PROCEDURE SP_Reintentos(p_RutUsuario IN VARCHAR2, p_IdPerfil IN INTEGER, 
                       p_Son OUT INTEGER, p_Activo OUT CHAR,
                       p_glosa OUT VARCHAR2,p_estado OUT INTEGER)
AS
p_IdUsuario INTEGER;
BEGIN 
/**************************************************************************************************************
   NAME:       PKG_USUARIO.SP_Reintetos
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       29/09/2020     a.mejiasc@alumnos.duoc.cl       1. Reintentos deben ser indicados en el usuario
***************************************************************************************************************/



SELECT u.IdUsuario INTO p_IdUsuario FROM Usuario u
INNER JOIN Perfil p on (p.IdPerfil = u.IdPerfil)
WHERE p.IdPerfil = p_IdPerfil
AND Rut = p_RutUsuario;

p_estado := 0;
p_glosa := 'Exito';


UPDATE Usuario SET Intentos = Intentos + 1
WHERE IdUsuario = p_IdUsuario
AND CambiaClave = '0';

UPDATE Usuario SET activo = CASE WHEN intentos<3 THEN '1' else '0' end 
WHERE IdUsuario = p_IdUsuario;

SELECT 1 AS Son, Activo INTO p_Son, p_Activo
FROM Usuario
WHERE IdUsuario = p_IdUsuario;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'Usuario no existe';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Reintentos;
PROCEDURE SP_LoginUsuario(
p_RutUsuario IN VARCHAR2,
p_ClaveUsuario IN VARCHAR2,
p_IdPerfil IN INTEGER,
p_IdUsuario OUT INTEGER,
p_glosa OUT VARCHAR2,
p_estado OUT INTEGER) 
AS
BEGIN 
/**************************************************************************************************************
   NAME:       PKG_USUARIO.SP_LoginUsuario
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       29/09/2020     a.mejiasc@alumnos.duoc.cl       1. procedimiento que revisa la existencia del usuario logueado
***************************************************************************************************************/
p_estado := 0;
p_glosa := 'Exito';

SELECT Usuario.IdUsuario INTO  p_IdUsuario FROM Usuario      
WHERE USUARIO.Rut = p_RutUsuario
AND USUARIO.CLAVE = p_ClaveUsuario
AND USUARIO.IdPerfil = p_IdPerfil
AND ESTADO = '1';

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'Usuario no existe';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_LoginUsuario;
PROCEDURE SP_GeneraSesion(p_idUsuario IN INTEGER, p_Fecha IN VARCHAR2,
                          p_Guid IN VARCHAR2, p_Json IN Varchar2,
                          p_glosa OUT VARCHAR2,p_estado OUT INTEGER)
AS 
BEGIN
/**************************************************************************************************************
   NAME:       PKG_USUARIO.SP_GeneraSesion
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       01/10/2020     a.mejiasc@alumnos.duoc.cl       1. procedimiento que crea la sesión en la BD
***************************************************************************************************************/
p_estado := 0;
p_glosa := 'Exito';

    UPDATE Usuario SET Intentos = 0
	WHERE IdUsuario = p_idUsuario;

	INSERT INTO Sesion (IdSesion, IdUsuario, Remote_Addr, Fecha )
	VALUES (p_Guid, p_idUsuario, p_Json, p_Fecha);
    
    EXCEPTION 
    WHEN OTHERS THEN
        p_estado := -1;
        p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_GeneraSesion;                        
END "PKG_USUARIO";
