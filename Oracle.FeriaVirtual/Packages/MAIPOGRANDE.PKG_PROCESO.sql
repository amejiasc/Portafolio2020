-- ****** Objeto: Paquete MAIPOGRANDE.PKG_PROCESO Fecha de Script: 06-11-2020 16:35:59 ******
CREATE OR REPLACE PACKAGE "MAIPOGRANDE"."PKG_PROCESO" AS  
PROCEDURE SP_Crear( p_COMISION IN NUMBER, 
                    p_VALORADUANA IN NUMBER, 
                    p_PAGOPORSERVICIO IN NUMBER, 
                    p_PAGOTRANSPORTISTA IN NUMBER, 
                    p_FECHAPROCESO IN DATE, 
                    p_FECHAFINPROCESO IN DATE, 
                    p_ESTADOPROCESO IN VARCHAR2, 
                    p_IDORDEN IN NUMBER, 
                    p_IDUSUARIO IN NUMBER, 
                    p_IDPROCESO OUT NUMBER,
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER);
PROCEDURE SP_Modificar( p_IDPROCESO IN NUMBER, 
                        p_COMISION IN NUMBER, 
                        p_VALORADUANA IN NUMBER, 
                        p_PAGOPORSERVICIO IN NUMBER, 
                        p_PAGOTRANSPORTISTA IN NUMBER, 
                        p_FECHAPROCESO IN DATE, 
                        p_FECHAFINPROCESO IN DATE, 
                        p_ESTADOPROCESO IN VARCHAR2, 
                        p_IDORDEN IN NUMBER, 
                        p_IDUSUARIO IN NUMBER,                     
                        p_glosa OUT VARCHAR2, p_estado OUT INTEGER);
END "PKG_PROCESO";
