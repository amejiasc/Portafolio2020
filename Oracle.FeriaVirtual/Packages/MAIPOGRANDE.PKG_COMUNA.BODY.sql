-- ****** Objeto: Cuerpo del Paquete MAIPOGRANDE.PKG_COMUNA.BODY Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE BODY "MAIPOGRANDE"."PKG_COMUNA" AS 

PROCEDURE SP_Crear_Comuna(p_IdComuna IN INTEGER, p_CodigoComuna IN INTEGER, p_IdRegion IN INTEGER, p_NombreComuna in VARCHAR2,p_NombreCiudad IN VARCHAR2, p_NombreRegion IN VARCHAR2,p_estado OUT INTEGER, p_glosa OUT VARCHAR2)
AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_COMUNA.SP_Crear_Comuna
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       18/10/2020     j.caroo@alumnos.duoc.cl         1. Crea Comuna 
***************************************************************************************************************/
p_estado := 0;
p_glosa := 'Exito';

INSERT INTO COMUNA (IDCOMUNA, CODIGOCOMUNA, IDREGION, NOMBRECOMUNA, NOMBRECIUDAD, NOMBREREGION) 
VALUES (p_IdComuna, p_CodigoComuna, p_IdRegion, p_NombreComuna, p_NombreCiudad, p_NombreRegion);

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible crear Comuna';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible crear Comuna';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Crear_Comuna;

PROCEDURE SP_Listar_Comuna(p_idComuna IN NUMBER,OUT_PC_GET_Comuna OUT SYS_REFCURSOR,p_estado out number, p_glosa out VARCHAR2)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_COMUNA.SP_Listar_Comuna
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       19/10/2020     j.caroo@alumnos.duoc.cl         1. Listar Comuna 
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

open OUT_PC_GET_Comuna for 
select IDCOMUNA ,
CODIGOCOMUNA ,
IDREGION ,
NOMBRECOMUNA ,
NOMBRECIUDAD ,
NOMBREREGION   from comuna
where IDCOMUNA = p_idComuna;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Listar Comuna';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Listar Comuna';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Listar_Comuna;


PROCEDURE SP_Modificar_Comuna(p_IdComuna IN number, p_CodigoComuna IN number, p_IdRegion IN number, p_NombreComuna in VARCHAR2,p_NombreCiudad IN VARCHAR2, p_NombreRegion IN VARCHAR2,p_estado OUT number, p_glosa OUT VARCHAR2)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_COMUNA.SP_Modificar_Comuna
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       19/10/2020     j.caroo@alumnos.duoc.cl         1. Modificar Comuna 
***************************************************************************************************************/
p_estado := 0;
p_glosa := 'Exito';

update comuna set
IDCOMUNA = p_IdComuna,
CODIGOCOMUNA=p_CodigoComuna,
IDREGION=p_IdRegion,
NOMBRECOMUNA=p_NombreComuna,
NOMBRECIUDAD=p_NombreCiudad,
NOMBREREGION=p_NombreRegion
where comuna.idcomuna = p_IdComuna;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Modificar Comuna';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Modificar Comuna';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);


END SP_Modificar_Comuna;

PROCEDURE SP_Eliminar_Comuna(p_IdComuna IN number,p_estado out number, p_glosa out VARCHAR2)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_COMUNA.SP_Eliminar_Comuna
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       18/10/2020     j.caroo@alumnos.duoc.cl         1. Eliminar Comuna 
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

delete from comuna where idcomuna = p_IdComuna;


if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Eliminar Comuna';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Eliminar Comuna';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Eliminar_Comuna;
end "PKG_COMUNA";
