-- ****** Objeto: Cuerpo del Paquete MAIPOGRANDE.PKG_CAMION.BODY Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE BODY "MAIPOGRANDE"."PKG_CAMION" AS
 PROCEDURE SP_Crear
(p_PesoMaximo IN camion.pesomaximo%type, p_Refrigera IN camion.refrigera%type, p_IdUsuario IN camion.idusuario%type,
                   p_IdCamion OUT INTEGER, p_glosa OUT VARCHAR2, p_estado OUT INTEGER)
AS
BEGIN

/**************************************************************************************************************
   NAME:       PKG_CAMION.SP_Crear
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     -------------------            ----------------------------------------------
   1.0        18/10/2020     k.alvarezb@alumnos.duoc.cl     1. Procedimiento para crear un camion

***************************************************************************************************************/

p_estado := 0;
p_glosa := 'PKG_CAMION.SP_Crear ejecutado exitosamente';

p_IdCamion:=SQ_CAMION.nextval;


INSERT INTO CAMION(
    IdCamion,
    PesoMaximo,
    Refrigera,
    IdUsuario
)
VALUES (
    p_IdCamion,
    p_PesoMaximo,
    p_Refrigera,
    p_IdUsuario
);


if SQL%ROWCOUNT=0 then
p_estado := 1;
p_glosa := 'No fue posible crear el camion';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible crear el camion';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Crear;
PROCEDURE SP_Modificar(p_PesoMaximo IN camion.pesomaximo%type, p_Refrigera IN camion.refrigera%type,
                   p_IdCamion IN camion.idcamion%type, p_glosa OUT VARCHAR2, p_estado OUT INTEGER)
                   
AS
BEGIN

/**************************************************************************************************************
   NAME:       PKG_CAMION.SP_Modificar
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     -------------------            ----------------------------------------------
   1.0        18/10/2020     k.alvarezb@alumnos.duoc.cl     1. Procedimiento para modificar un camion

***************************************************************************************************************/

p_estado := 0;
p_glosa := 'PKG_CAMION.SP_Modificar ejecutado exitosamente';

Update Camion SET PesoMaximo = p_PesoMaximo, 
                 Refrigera = p_Refrigera
WHERE IdCamion = p_IdCamion;


if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible modificar el camion';
end if;


EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible modificar el camion';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Modificar;
PROCEDURE SP_Listar(p_IdUsuario IN camion.idusuario%type,
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER, p_leer_camion OUT SYS_REFCURSOR)
                   
AS
BEGIN

/**************************************************************************************************************
   NAME:       PKG_CAMION.SP_Listar
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     -------------------            ----------------------------------------------
   1.0        18/10/2020     k.alvarezb@alumnos.duoc.cl     1. Procedimiento para listar camiones

***************************************************************************************************************/

p_estado := 0;
p_glosa := 'PKG_CAMION.SP_Listar ejecutado exitosamente';

OPEN p_leer_camion FOR
    SELECT 
    idcamion,
    pesomaximo,
    refrigera
    FROM camion WHERE idusuario = p_IdUsuario;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible listar camiones';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible listar camiones';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Listar;
PROCEDURE SP_Leer(p_IdCamion IN camion.idcamion%type,
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER, p_leer_camion OUT SYS_REFCURSOR)
AS
BEGIN

/**************************************************************************************************************
   NAME:       PKG_CAMION.SP_Leer
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     -------------------            ----------------------------------------------
   1.0        18/10/2020     k.alvarezb@alumnos.duoc.cl     1. Procedimiento para leer un camion

***************************************************************************************************************/

p_estado := 0;
p_glosa := 'PKG_CAMION.SP_Leer ejecutado exitosamente';

OPEN p_leer_camion FOR
    SELECT 
    idcamion,
    pesomaximo,
    refrigera
    FROM camion WHERE idcamion = p_IdCamion;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible leer el camion';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible leer el camion';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);
END SP_Leer;
PROCEDURE SP_Eliminar(p_IdCamion IN camion.idcamion%type,
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_CAMION.SP_Eliminar
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       18/10/2020     k.alvarezb@alumnos.duoc.cl         1. Eliminar camion 
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'PKG_CAMION.SP_Eliminar ejecutado exitosamente';

DELETE FROM camion where camion.idcamion = p_IdCamion;


if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible eliminar el camion';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible eliminar el camion';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Eliminar;
END "PKG_CAMION";
