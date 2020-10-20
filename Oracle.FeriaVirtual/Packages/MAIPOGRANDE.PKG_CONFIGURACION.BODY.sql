-- ****** Objeto: Cuerpo del Paquete MAIPOGRANDE.PKG_CONFIGURACION.BODY Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE BODY "MAIPOGRANDE"."PKG_CONFIGURACION" AS 

PROCEDURE SP_Crear_Configuracion(p_Nombrepaso IN VARCHAR2, p_TipoPaso IN VARCHAR2, p_IdAnterior in number,p_IdPosterior IN Number,p_IdConfiguracion out number, p_estado OUT number, p_glosa OUT VARCHAR2)
AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_CONFIGURACION.SP_Crear_Configuracion
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       18/10/2020     j.caroo@alumnos.duoc.cl         1. Crea Configuracion 
***************************************************************************************************************/
p_estado := 0;
p_glosa := 'Exito';

p_IdConfiguracion:=SQ_Configuracion.nextval;

INSERT INTO maipogrande.configuracion (
ID,
NOMBREPASO,
TIPO,
IDANTERIOR,
IDPOSTERIOR
) 
VALUES (
p_IdConfiguracion, 
p_Nombrepaso, 
p_TipoPaso, 
p_IdAnterior,
p_IdPosterior);




if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible crear Configuracion';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible crear Configuracion';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Crear_Configuracion;

PROCEDURE SP_Listar_Configuracion(p_IdConfiguracion IN NUMBER,p_estado out number, p_glosa out VARCHAR2,OUT_PC_GET_Configuracion OUT SYS_REFCURSOR)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_CONFIGURACION.SP_Listar_Configuracion
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       19/10/2020     j.caroo@alumnos.duoc.cl         1. Listar Configuracion 
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

open OUT_PC_GET_Configuracion for 
select ID ,
NOMBREPASO ,
TIPO ,
IDANTERIOR ,
IDPOSTERIOR 
from configuracion
where ID = p_IdConfiguracion;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Listar COnfiguracion';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Listar COnfiguracion';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Listar_Configuracion;

PROCEDURE SP_Modificar_Configuracion(p_IdConfiguracion IN number, p_Nombrepaso IN VARCHAR2, p_TipoPaso IN VARCHAR2, p_IdAnterior in number,p_IdPosterior IN Number,p_estado OUT number, p_glosa OUT VARCHAR2)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_CONFIGURACION.SP_Modificar_Configuracion
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       19/10/2020     j.caroo@alumnos.duoc.cl         1. Modificar Configuracion 
***************************************************************************************************************/
p_estado := 0;
p_glosa := 'Exito';

update configuracion set 
ID=p_IdConfiguracion,
NOMBREPASO=p_Nombrepaso,
TIPO=p_TipoPaso,
IDANTERIOR=p_IdAnterior,
IDPOSTERIOR=p_IdPosterior
where id=p_IdConfiguracion;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Modificar Configuracion';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Modificar Configuracion';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);


END SP_Modificar_Configuracion;

PROCEDURE SP_Eliminar_Configuracion(p_IdConfiguracion IN INTEGER,p_estado OUT number, p_glosa OUT VARCHAR2)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_CONFIGURACION.SP_Eliminar_Configuracion
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       19/10/2020     j.caroo@alumnos.duoc.cl         1. Eliminar Configuracion 
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

delete from configuracion where ID = p_IdConfiguracion;


if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Eliminar Configuracion';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Eliminar Configuracion';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Eliminar_Configuracion;

END "PKG_CONFIGURACION";
