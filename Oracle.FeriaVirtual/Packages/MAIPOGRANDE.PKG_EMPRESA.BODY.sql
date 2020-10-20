-- ****** Objeto: Cuerpo del Paquete MAIPOGRANDE.PKG_EMPRESA.BODY Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE BODY "MAIPOGRANDE"."PKG_EMPRESA" AS 

PROCEDURE SP_Crear_Empresa(p_RutEmpresa IN VARCHAR2, p_NombreEmpresa IN VARCHAR2, p_Direccion IN VARCHAR2, p_Region in number,p_Ciudad IN VARCHAR2, p_Email IN VARCHAR2,p_Telefono in number,p_Celular in number,p_IdComuna in number,p_estado out number, p_glosa out VARCHAR2)
AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_EMPRESA.SP_Crear_Empresa
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       18/10/2020     j.caroo@alumnos.duoc.cl         1. Crea Empresa 
***************************************************************************************************************/
p_estado := 0;
p_glosa := 'Exito';

INSERT INTO maipogrande.empresa (
rutempresa, 
nombreempresa, 
direccion, 
region, 
CIUDAD, 
EMAIL,
telefono,
celular,
idcomuna
) 
VALUES (
p_RutEmpresa, 
p_NombreEmpresa, 
p_Direccion, 
p_Region,
p_Ciudad, 
p_Email,
p_Telefono,
p_Celular,
p_IdComuna);

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Listar Empresa';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Listar Empresa';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Crear_Empresa;

PROCEDURE SP_Listar_Empresa(p_RutEmpresa IN VARCHAR2,OUT_PC_GET_Empresa OUT SYS_REFCURSOR,p_estado out number, p_glosa out VARCHAR2)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_EMPRESA.SP_Listar_Empresa
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       18/10/2020     j.caroo@alumnos.duoc.cl         1. Listar Empresa 
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

open OUT_PC_GET_Empresa for 
select RUTEMPRESA ,
NOMBREEMPRESA ,
DIRECCION ,
REGION ,
CIUDAD ,
EMAIL ,
TELEFONO ,
CELULAR ,
IDCOMUNA  from empresa
where rutempresa = p_RutEmpresa;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Listar Empresa';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Listar Empresa';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Listar_Empresa;

PROCEDURE SP_Modificar_Empresa(p_RutEmpresa IN VARCHAR2, p_NombreEmpresa IN VARCHAR2, p_Direccion IN VARCHAR2, p_Region in number,p_Ciudad IN VARCHAR2, p_Email IN VARCHAR2,p_Telefono in number,p_Celular in number,p_IdComuna in number,p_estado out number, p_glosa out VARCHAR2)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_EMPRESA.SP_Modificar_Empresa
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       18/10/2020     j.caroo@alumnos.duoc.cl         1. Modificar Empresa 
***************************************************************************************************************/
p_estado := 0;
p_glosa := 'Exito';

update empresa set 
empresa.rutempresa = p_RutEmpresa, 
empresa.nombreempresa = p_NombreEmpresa, 
empresa.direccion = p_Direccion, 
empresa.region = p_Region,
empresa.ciudad = p_Ciudad, 
empresa.email = p_Email,
empresa.telefono = p_Telefono,
empresa.celular = p_Celular,
empresa.idcomuna = p_IdComuna
where empresa.rutempresa=p_RutEmpresa;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Modificar Empresa';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Modificar Empresa';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Modificar_Empresa;

PROCEDURE SP_Eliminar_Empresa(p_RutEmpresa IN VARCHAR2,p_estado out number, p_glosa out VARCHAR2)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_EMPRESA.SP_Eliminar_Empresa
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       18/10/2020     j.caroo@alumnos.duoc.cl         1. Eliminar Empresa 
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

delete from empresa where empresa.rutempresa = p_RutEmpresa;


if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Eliminar Empresa';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Eliminar Empresa';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Eliminar_Empresa;


END "PKG_EMPRESA";
