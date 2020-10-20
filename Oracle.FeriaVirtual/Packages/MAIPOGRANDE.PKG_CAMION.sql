-- ****** Objeto: Paquete MAIPOGRANDE.PKG_CAMION Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE "MAIPOGRANDE"."PKG_CAMION" AS 

PROCEDURE SP_Crear(p_PesoMaximo IN camion.pesomaximo%type, p_Refrigera IN camion.refrigera%type, p_IdUsuario IN camion.idusuario%type,
                   p_IdCamion OUT INTEGER, p_glosa OUT VARCHAR2, p_estado OUT INTEGER);                     
PROCEDURE SP_Modificar(p_PesoMaximo IN camion.pesomaximo%type, p_Refrigera IN camion.refrigera%type,
                   p_IdCamion IN camion.idcamion%type, p_glosa OUT VARCHAR2, p_estado OUT INTEGER);                  
PROCEDURE SP_Listar(p_IdUsuario IN camion.idusuario%type,
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER, p_leer_camion OUT SYS_REFCURSOR);
PROCEDURE SP_Leer(p_IdCamion IN camion.idcamion%type,
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER, p_leer_camion OUT SYS_REFCURSOR);
PROCEDURE SP_Eliminar(p_IdCamion IN camion.idcamion%type,
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER);
END "PKG_CAMION";
