-- ****** Objeto: Cuerpo del Paquete MAIPOGRANDE.PKG_ETAPA.BODY Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE BODY "MAIPOGRANDE"."PKG_ETAPA" AS
 PROCEDURE SP_Crear
                  (p_EstadoEtapa IN etapa.estadoetapa%type, 
                   p_FechaEtapa IN etapa.fechaetapa%type,
                   p_IdProceso IN etapa.idproceso%TYPE, 
                   p_Observacion IN etapa.observacion%TYPE,
                   p_IdConfiguracion IN etapa.id%TYPE,
                   p_IdEtapa OUT INTEGER,
                   p_glosa OUT VARCHAR2,
                   p_estado OUT INTEGER)
AS
BEGIN

/**************************************************************************************************************
   NAME:       PKG_ETAPA.SP_Crear
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     -------------------            ----------------------------------------------
   1.0        19/10/2020     s.devia@alumnos.duoc.cl     1. Procedimiento para modificar una Etapa

***************************************************************************************************************/

p_estado := 0;
p_glosa := 'PKG_ETAPA.SP_Crear ejecutado exitosamente';

p_IdEtapa:=SQ_ETAPA.nextval;


INSERT INTO ETAPA(
    IDETAPA, 
	ESTADOETAPA, 
	FECHAETAPA, 
	IDPROCESO, 
	OBSERVACION, 
	ID
)
VALUES (
    p_IdEtapa,
    p_EstadoEtapa, 
    p_FechaEtapa,
    p_IdProceso, 
    p_Observacion,
    p_IdConfiguracion    
);


if SQL%ROWCOUNT=0 then
p_estado := 1;
p_glosa := 'No fue posible crear el etapa';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible crear el etapa';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Crear;



PROCEDURE SP_Modificar(p_EstadoEtapa IN etapa.estadoetapa%type, 
                   p_FechaEtapa IN etapa.fechaetapa%type,
                   p_IdProceso IN etapa.idproceso%TYPE, 
                   p_Observacion IN etapa.observacion%TYPE,
                   p_IdConfiguracion IN etapa.id%TYPE,
                   p_IdEtapa IN INTEGER,
                   p_glosa OUT VARCHAR2,
                   p_estado OUT INTEGER)
                   
AS
BEGIN

/**************************************************************************************************************
   NAME:       PKG_ETAPA.SP_Modificar
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     -------------------            ----------------------------------------------
   1.0        19/10/2020     s.devia@alumnos.duoc.cl     1. Procedimiento para modificar una etapa

***************************************************************************************************************/

p_estado := 0;
p_glosa := 'PKG_ETAPA.SP_Modificar ejecutado exitosamente';

Update Etapa SET IDETAPA = p_IdEtapa, 
                 ESTADOETAPA = p_EstadoEtapa, 
                 FECHAETAPA = p_FechaEtapa, 
                 IDPROCESO = p_IdProceso, 
                 OBSERVACION = p_Observacion, 
                 ID = p_IdConfiguracion
WHERE IDETAPA = p_IdEtapa;


if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible modificar la etapa';
end if;


EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible modificar la etapa';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Modificar;



PROCEDURE SP_Listar(p_IdProceso IN INTEGER,
                    p_estado out number, 
                    p_glosa out VARCHAR2,
                    OUT_PC_GET_Etapa OUT SYS_REFCURSOR)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_ETAPA.SP_Listar
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       19/10/2020     s.devia@alumnos.duoc.cl         1. Listar Etapas 
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

open OUT_PC_GET_Etapa for 
select IDETAPA ,
ESTADOETAPA ,
FECHAETAPA ,
IDPROCESO ,
OBSERVACION,
ID
from etapa
where IDPROCESO = p_IdProceso;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Listar las Etapas';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Listar las Etapas';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Listar;


END "PKG_ETAPA";
