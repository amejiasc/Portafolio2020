-- ****** Objeto: Cuerpo del Paquete MAIPOGRANDE.PKG_ORDEN.BODY Fecha de Script: 16-10-2020 17:18:41 ******
CREATE OR REPLACE PACKAGE BODY "MAIPOGRANDE"."PKG_ORDEN" AS
PROCEDURE SP_CrearDetalle(p_IdOrden IN INTEGER, p_Cantidad IN INTEGER, p_IdCategoria IN INTEGER, 
                   p_Monto IN detalleorden.monto%type,
                   p_glosa OUT VARCHAR2, p_estado OUT INTEGER)
AS
BEGIN 
/**************************************************************************************************************
   NAME:       PKG_ORDEN.SP_Crear
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       16/10/2020     a.mejiasc@alumnos.duoc.cl       1. Crea Detalle Orden 
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

INSERT INTO DetalleOrden (IdOrden, Cantidad, Monto, IdCategoria) 
VALUES (p_IdOrden, p_Cantidad, p_Monto, p_IdCategoria);


if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible crear detalle de la orden';
end if;


EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible crear detalle de la orden';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_CrearDetalle;
PROCEDURE SP_Crear(p_IdClienteExterno IN INTEGER, p_IdClienteInterno IN INTEGER, 
                   p_EstadoOrden IN VARCHAR2, p_PrecioVenta IN orden.precioventa%type,
                   p_IdOrden OUT INTEGER, p_glosa OUT VARCHAR2, p_estado OUT INTEGER)
AS
BEGIN 
/**************************************************************************************************************
   NAME:       PKG_ORDEN.SP_Crear
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       16/10/2020     a.mejiasc@alumnos.duoc.cl       1. Crea Orden de acuerdo al perfil del usuario
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

p_IdOrden := SQ_ORDEN.nextval;
if p_IdClienteExterno<>0 then
    INSERT INTO ORDEN (IdOrden, FechaOrden, Estado, PrecioVenta, IdClienteExterno, FirmaContrato) 
    VALUES (p_IdOrden, sysdate, p_EstadoOrden, p_PrecioVenta, p_IdClienteExterno, '0');
else
    INSERT INTO ORDEN (IdOrden, FechaOrden, Estado, PrecioVenta, IdClienteInterno, FirmaContrato) 
    VALUES (p_IdOrden, sysdate, p_EstadoOrden, p_PrecioVenta, p_IdClienteInterno, '0');
end if;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible crear la orden';
end if;


EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible crear la orden';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Crear;
PROCEDURE SP_Modificar(p_IdOrden IN INTEGER,
                   p_EstadoOrden IN VARCHAR2, p_PrecioVenta IN orden.precioventa%type,
                   p_FirmaContrato IN CHAR,
                   p_glosa OUT VARCHAR2, p_estado OUT INTEGER)
AS
BEGIN 
/**************************************************************************************************************
   NAME:       PKG_ORDEN.SP_Modificar
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       16/10/2020     a.mejiasc@alumnos.duoc.cl       1. Modfuca Orden de acuerdo al perfil del usuario
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

Update Orden SET precioventa = p_PrecioVenta, 
                 Estado=p_EstadoOrden, 
                 FirmaContrato=p_FirmaContrato, 
                 FechaFirmaContrato=sysdate
WHERE IdOrden = p_IdOrden;


if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible crear la orden';
end if;


EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible crear la orden';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Modificar;
END "PKG_ORDEN";
