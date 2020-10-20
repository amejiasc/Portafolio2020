-- ****** Objeto: Paquete MAIPOGRANDE.PKG_ETAPA Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE "MAIPOGRANDE"."PKG_ETAPA" AS 

PROCEDURE SP_Crear(p_EstadoEtapa IN etapa.estadoetapa%type, 
                   p_FechaEtapa IN etapa.fechaetapa%type,
                   p_IdProceso IN etapa.idproceso%TYPE, 
                   p_Observacion IN etapa.observacion%TYPE,
                   p_IdConfiguracion IN etapa.id%TYPE,
                   p_IdEtapa OUT INTEGER,
                   p_glosa OUT VARCHAR2,
                   p_estado OUT INTEGER);  

PROCEDURE SP_Modificar(p_EstadoEtapa IN etapa.estadoetapa%type, 
                   p_FechaEtapa IN etapa.fechaetapa%type,
                   p_IdProceso IN etapa.idproceso%TYPE, 
                   p_Observacion IN etapa.observacion%TYPE,
                   p_IdConfiguracion IN etapa.id%TYPE,
                   p_IdEtapa IN INTEGER,
                   p_glosa OUT VARCHAR2,
                   p_estado OUT INTEGER);
                   
PROCEDURE SP_Listar(p_IdProceso IN INTEGER,
                    p_estado out number, 
                    p_glosa out VARCHAR2,
                    OUT_PC_GET_Etapa OUT SYS_REFCURSOR);

END "PKG_ETAPA";
