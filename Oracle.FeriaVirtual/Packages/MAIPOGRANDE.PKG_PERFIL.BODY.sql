-- ****** Objeto: Cuerpo del Paquete MAIPOGRANDE.PKG_PERFIL.BODY Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE BODY "MAIPOGRANDE"."PKG_PERFIL" AS
PROCEDURE SP_CREAR(
  P_NOMBREPERFIL 			IN VARCHAR2,
  P_CODIGOPERFIL 			IN VARCHAR2,
  P_SERVICIOPERFIL 		IN VARCHAR2,
  P_IDPERFIL 			    OUT NUMBER,
  P_ESTADO 		        OUT NUMBER,
  P_GLOSA 	          OUT VARCHAR2
) AS

/**************************************************************************************************************
   NAME:       SP_CREAR
   PURPOSE:    Crear un perfil de usuario

   REVISIONS:
   Ver        Date           Author                    Description
   ---------  ----------     -------------------       ----------------------------------------------
   1.0        19/10/2020     e.lealp@alumnos.duoc.cl   1. Creación procedimiento que crea un nuevo Perfil

***************************************************************************************************************/

BEGIN
    P_ESTADO := 0;
    P_GLOSA := 'SP_CREAR_PERFIL ejecutado exitosamente';
    P_IDPERFIL := SQ_PERFIL.NEXTVAL;

INSERT INTO MAIPOGRANDE.PERFIL
  (
    IDPERFIL,
    NOMBREPERFIL,
    CODIGOPERFIL,
    SERVICIOPERFIL,
    ESTADO
  )
  VALUES
  (
  P_IDPERFIL,
  P_NOMBREPERFIL,
  P_CODIGOPERFIL,
  P_SERVICIOPERFIL,
  P_ESTADO
  );

    EXCEPTION
        WHEN OTHERS THEN
            P_ESTADO := -1;
            P_GLOSA := MAIPOGRANDE.FN_GET_GLOSA_ERROR;
END SP_CREAR;

PROCEDURE SP_ELIMINAR(
  P_IDPERFIL IN NUMBER,
  P_ESTADO OUT NUMBER,
  P_GLOSA OUT VARCHAR2
) AS
/**************************************************************************************************************
   NAME:       SP_ELIMINAR
   PURPOSE:   Eliminar un perfil

   REVISIONS:
   Ver        Date           Author                               Description
   ---------  ----------     -------------------                  ----------------------------------------------
   1.0        19/10/2020     e.lealp@alumnos.duoc.cl      				1. procedimiento que elimina un perfil
***************************************************************************************************************/
BEGIN
    p_estado := 0;
    p_glosa := 'SP_ELIMINAR ejecutado exitosamente';

DELETE FROM MAIPOGRANDE.PERFIL
WHERE IDPERFIL     = P_IDPERFIL;

EXCEPTION
        WHEN OTHERS THEN
          P_ESTADO := -1;
          P_GLOSA := MAIPOGRANDE.FN_GET_GLOSA_ERROR;

END SP_ELIMINAR;

PROCEDURE SP_MODIFICAR (
  P_IDPERFIL 			    IN NUMBER,
  P_NOMBREPERFIL 			IN VARCHAR2,
  P_CODIGOPERFIL 			IN VARCHAR2,
  P_SERVICIOPERFIL 		IN VARCHAR2,
  P_ESTADOPERFIL 		        IN NUMBER,
  P_ESTADO               OUT NUMBER,
  P_GLOSA 	          OUT VARCHAR2
) AS
/**************************************************************************************************************
   NAME:       SP_MODIFICAR
   PURPOSE: Moficiar un perfil

   REVISIONS:
   Ver        Date           Author                               Description
   ---------  ----------     -------------------                  ----------------------------------------------
   1.0        19/10/2020     e.lealp@alumnos.duoc.cl      				1. procedimiento que modifica un perfil
***************************************************************************************************************/

BEGIN
    P_ESTADO := 0;
    P_GLOSA := 'SP_MODIFICAR ejecutado exitosamente';

    UPDATE MAIPOGRANDE.PERFIL SET 
      NOMBREPERFIL = P_NOMBREPERFIL,
      CODIGOPERFIL = P_CODIGOPERFIL,
      SERVICIOPERFIL = P_SERVICIOPERFIL,
      ESTADO = P_ESTADOPERFIL
      WHERE IDPERFIL = P_IDPERFIL;

    EXCEPTION
        WHEN OTHERS THEN
			P_ESTADO := -1;
			P_GLOSA := MAIPOGRANDE.FN_GET_GLOSA_ERROR;	


END SP_MODIFICAR;
END "PKG_PERFIL";
