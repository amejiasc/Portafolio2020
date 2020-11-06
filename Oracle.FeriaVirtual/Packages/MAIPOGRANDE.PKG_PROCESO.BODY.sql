-- ****** Objeto: Cuerpo del Paquete MAIPOGRANDE.PKG_PROCESO.BODY Fecha de Script: 06-11-2020 16:35:59 ******
CREATE OR REPLACE PACKAGE BODY "MAIPOGRANDE"."PKG_PROCESO" AS 
PROCEDURE SP_Modificar (p_IDPROCESO IN NUMBER, 
                        p_COMISION IN NUMBER, 
                        p_VALORADUANA IN NUMBER, 
                        p_PAGOPORSERVICIO IN NUMBER, 
                        p_PAGOTRANSPORTISTA IN NUMBER, 
                        p_FECHAPROCESO IN DATE, 
                        p_FECHAFINPROCESO IN DATE, 
                        p_ESTADOPROCESO IN VARCHAR2, 
                        p_IDORDEN IN NUMBER, 
                        p_IDUSUARIO IN NUMBER,                     
                        p_glosa OUT VARCHAR2, p_estado OUT INTEGER)
AS
BEGIN 
/**************************************************************************************************************
   NAME:       PKG_PROCESO.SP_Crear
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       26/10/2020     a.mejiasc@alumnos.duoc.cl       1. Modifica proceso asociado a una orden
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

UPDATE Proceso
SET
COMISION= p_COMISION,
VALORADUANA= p_VALORADUANA,
PAGOPORSERVICIO= p_PAGOPORSERVICIO,
PAGOTRANSPORTISTA= p_PAGOTRANSPORTISTA,
FECHAPROCESO= p_FECHAPROCESO,
FECHAFINPROCESO= p_FECHAFINPROCESO,
ESTADOPROCESO= p_ESTADOPROCESO,
IDORDEN= p_IDORDEN,
IDUSUARIO= p_IDUSUARIO
WHERE IDPROCESO = p_IDPROCESO;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'Proceso seleccionado no existe';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Modificar el proceso';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Modificar;
PROCEDURE SP_Crear (p_COMISION IN NUMBER, 
                    p_VALORADUANA IN NUMBER, 
                    p_PAGOPORSERVICIO IN NUMBER, 
                    p_PAGOTRANSPORTISTA IN NUMBER, 
                    p_FECHAPROCESO IN DATE, 
                    p_FECHAFINPROCESO IN DATE, 
                    p_ESTADOPROCESO IN VARCHAR2, 
                    p_IDORDEN IN NUMBER, 
                    p_IDUSUARIO IN NUMBER, 
                    p_IDPROCESO OUT NUMBER,
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER)
AS
BEGIN 
/**************************************************************************************************************
   NAME:       PKG_PRODUCTO.SP_Crear
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       16/10/2020     a.mejiasc@alumnos.duoc.cl       1. Crea proceso asociado a una orden generada
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

p_IDPROCESO := SQ_PROCESO.nextval;

INSERT INTO PROCESO 
(
IDPROCESO,
COMISION,
VALORADUANA,
PAGOPORSERVICIO,
PAGOTRANSPORTISTA,
FECHAPROCESO,
FECHAFINPROCESO,
ESTADOPROCESO,
IDORDEN,
IDUSUARIO
) 
VALUES (
p_IDPROCESO,
p_COMISION,
p_VALORADUANA,
p_PAGOPORSERVICIO,
p_PAGOTRANSPORTISTA,
p_FECHAPROCESO,
p_FECHAFINPROCESO,
p_ESTADOPROCESO,
p_IDORDEN,
p_IDUSUARIO
);

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible crear el proceso';
end if;


EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible crear el proceso';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Crear;


END "PKG_PROCESO";
