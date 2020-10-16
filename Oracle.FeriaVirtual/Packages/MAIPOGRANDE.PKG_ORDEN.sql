-- ****** Objeto: Paquete MAIPOGRANDE.PKG_ORDEN Fecha de Script: 16-10-2020 17:18:41 ******
CREATE OR REPLACE PACKAGE "MAIPOGRANDE"."PKG_ORDEN" AS
PROCEDURE SP_Crear(p_IdClienteExterno IN INTEGER, p_IdClienteInterno IN INTEGER, 
                   p_EstadoOrden IN VARCHAR2, p_PrecioVenta IN orden.precioventa%type,
                   p_IdOrden OUT INTEGER, p_glosa OUT VARCHAR2, p_estado OUT INTEGER);
PROCEDURE SP_CrearDetalle(p_IdOrden IN INTEGER, p_Cantidad IN INTEGER, p_IdCategoria IN INTEGER, 
                   p_Monto IN detalleorden.monto%type,
                   p_glosa OUT VARCHAR2, p_estado OUT INTEGER);   
PROCEDURE SP_Modificar(p_IdOrden IN INTEGER,
                   p_EstadoOrden IN VARCHAR2, p_PrecioVenta IN orden.precioventa%type,
                   p_FirmaContrato IN CHAR,
                   p_glosa OUT VARCHAR2, p_estado OUT INTEGER);                   
END "PKG_ORDEN";
/**/
