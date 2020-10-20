-- ****** Objeto: Cuerpo del Paquete MAIPOGRANDE.PKG_Detalle_Subasta.BODY Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE BODY "MAIPOGRANDE"."PKG_Detalle_Subasta" AS 
PROCEDURE SP_Crear_DetalleSubasta( p_fecha IN detallesubasta.fecha%TYPE, p_Estado_detalle_subasta IN VARCHAR2, p_idSubasta in number, p_idUsuario in number,p_Monto IN detallesubasta.montooferta%TYPE,p_IdDetalle_Subasta out number, p_glosa OUT VARCHAR2, p_estado OUT INTEGER)
                   
AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_Detalle_Subasta.SP_Crear_DetalleSubasta
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       19/10/2020     j.caroo@alumnos.duoc.cl         1. Crea detalle subasta 
***************************************************************************************************************/
p_estado := 0;
p_glosa := 'Exito';
p_IdDetalle_Subasta:=SQ_Detalle_Subasta.nextval;

INSERT INTO detallesubasta (IDDETALLE, MONTOOFERTA, FECHA, ESTADO, IDSUBASTA, IDUSUARIO) 
VALUES (p_IdDetalle_Subasta, p_Monto,p_fecha,p_Estado_detalle_subasta, p_idSubasta,p_idUsuario);



if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible crear detalle subasta';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible crear Detalle Subasta';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Crear_DetalleSubasta;

PROCEDURE SP_Listar_DetalleSubasta(p_IdDetalle_Subasta IN NUMBER,OUT_PC_GET_DetalleSubasta OUT SYS_REFCURSOR,p_estado out number, p_glosa out VARCHAR2)
AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_Detalle_Subasta.SP_Listar_DetalleSubasta
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       19/10/2020     j.caroo@alumnos.duoc.cl         1. Listar DetalleSubasta 
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

open OUT_PC_GET_DetalleSubasta for 
select IDDETALLE ,
MONTOOFERTA ,
FECHA ,
ESTADO ,
IDSUBASTA ,
IDUSUARIO 
from detallesubasta
where IDDETALLE = p_IdDetalle_Subasta;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Listar DetalleSubasta';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Listar DetalleSubasta';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Listar_DetalleSubasta;

PROCEDURE SP_Modificar_DetalleSubasta(p_IdDetalle_Subasta IN number, p_fecha IN detallesubasta.fecha%TYPE, p_Estado_detalle_subasta IN VARCHAR2, p_idSubasta in number, p_idUsuario in number,p_Monto IN detallesubasta.montooferta%TYPE, p_glosa OUT VARCHAR2, p_estado OUT INTEGER)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_Detalle_Subasta.SP_Modificar_DetalleSubasta
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       19/10/2020     j.caroo@alumnos.duoc.cl         1. Modificar DetalleSubasta 
***************************************************************************************************************/
p_estado := 0;
p_glosa := 'Exito';

update detallesubasta set
IDDETALLE= p_IdDetalle_Subasta,
FECHA= p_fecha,
ESTADO=p_Estado_detalle_subasta,
IDSUBASTA=p_idSubasta,
IDUSUARIO=p_idUsuario,
MONTOOFERTA=p_Monto
where detallesubasta.iddetalle = p_IdDetalle_Subasta;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Modificar DetalleSubasta';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Modificar DetalleSubasta';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);


END SP_Modificar_DetalleSubasta;

PROCEDURE SP_Eliminar_DetalleSubasta(p_IdDetalle_Subasta IN number, p_glosa OUT VARCHAR2, p_estado OUT number)

AS
BEGIN
/**************************************************************************************************************
   NAME:       PKG_Detalle_Subasta.SP_Eliminar_DetalleSubasta
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       19/10/2020     j.caroo@alumnos.duoc.cl         1. Eliminar DetalleSubasta 
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

delete from detallesubasta where IDDETALLE = p_IdDetalle_Subasta;


if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Eliminar DetalleSubasta';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Eliminar DetalleSubasta';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Eliminar_DetalleSubasta;

END "PKG_Detalle_Subasta";
