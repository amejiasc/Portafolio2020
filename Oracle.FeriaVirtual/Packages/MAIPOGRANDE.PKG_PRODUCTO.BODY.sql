-- ****** Objeto: Cuerpo del Paquete MAIPOGRANDE.PKG_PRODUCTO.BODY Fecha de Script: 26-10-2020 16:22:17 ******
CREATE OR REPLACE PACKAGE BODY "MAIPOGRANDE"."PKG_PRODUCTO" AS
PROCEDURE SP_Modificar (p_IdProducto IN INTEGER,
                    p_CODIGOPRODUCTO IN VARCHAR2, 
                    p_NOMBRE IN VARCHAR2, 
                    p_DESCRIPCION IN VARCHAR2, 
                    p_STOCK IN NUMBER, 
                    p_VALORUNITARIO IN NUMBER, 
                    p_TIPOVENTA IN VARCHAR2, 
                    p_CALIDAD IN NUMBER, 
                    p_FECHACADUCIDAD IN DATE, 
                    p_IDCATEGORIA IN NUMBER, 
                    p_IDUSUARIO IN NUMBER,                     
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER)
AS
BEGIN 
/**************************************************************************************************************
   NAME:       PKG_PRODUCTO.SP_Crear
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       16/10/2020     a.mejiasc@alumnos.duoc.cl       1. Modifica producto asociado al productor
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

UPDATE PRODUCTO 
SET
CODIGOPRODUCTO = p_CODIGOPRODUCTO,
NOMBRE = p_NOMBRE,
DESCRIPCION = p_DESCRIPCION,
STOCK = p_STOCK,
VALORUNITARIO = p_VALORUNITARIO,
TIPOVENTA = p_TIPOVENTA,
CALIDAD = p_CALIDAD,
FECHACADUCIDAD = p_FECHACADUCIDAD,
FECHAMODIFICACION = sysdate,
IDCATEGORIA = p_IDCATEGORIA,
IDUSUARIO = p_IDUSUARIO
WHERE IDPRODUCTO = p_IDPRODUCTO;

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible Modificar el producto';
end if;

EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible Modificar el producto';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Modificar;
PROCEDURE SP_Crear (p_CODIGOPRODUCTO IN VARCHAR2, 
                    p_NOMBRE IN VARCHAR2, 
                    p_DESCRIPCION IN VARCHAR2, 
                    p_STOCK IN NUMBER, 
                    p_VALORUNITARIO IN NUMBER, 
                    p_TIPOVENTA IN VARCHAR2, 
                    p_CALIDAD IN NUMBER, 
                    p_FECHACADUCIDAD IN DATE, 
                    p_IDCATEGORIA IN NUMBER, 
                    p_IDUSUARIO IN NUMBER, 
                    p_IdProducto OUT INTEGER,
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER)
AS
BEGIN 
/**************************************************************************************************************
   NAME:       PKG_PRODUCTO.SP_Crear
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       16/10/2020     a.mejiasc@alumnos.duoc.cl       1. Crea producto asociado al productor
***************************************************************************************************************/

p_estado := 0;
p_glosa := 'Exito';

p_IdProducto := SQ_PRODUCTO.nextval;

INSERT INTO PRODUCTO 
(
IDPRODUCTO,
CODIGOPRODUCTO,
NOMBRE,
DESCRIPCION,
STOCK,
VALORUNITARIO,
TIPOVENTA,
CALIDAD,
FECHACADUCIDAD,
FECHACREACION,
IDCATEGORIA,
IDUSUARIO
) 
VALUES (
p_IDPRODUCTO,
p_CODIGOPRODUCTO,
p_NOMBRE,
p_DESCRIPCION,
p_STOCK,
p_VALORUNITARIO,
p_TIPOVENTA,
p_CALIDAD,
p_FECHACADUCIDAD,
sysdate,
p_IDCATEGORIA,
p_IDUSUARIO
);

if SQL%ROWCOUNT=0 then
    p_estado := 1;
    p_glosa := 'No fue posible crear el producto';
end if;


EXCEPTION when no_data_found THEN
p_estado := 1;
p_glosa := 'No fue posible crear el producto';
WHEN OTHERS THEN
p_estado := -1;
p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Crear;

PROCEDURE SP_Listar (p_IdProductor IN INTEGER,
                     p_glosa OUT VARCHAR2, p_estado OUT INTEGER, pc_productos OUT sys_refcursor)
AS 
BEGIN
/**************************************************************************************************************
   NAME:       PKG_PRODUCTO.SP_Listar
   PURPOSE:

   REVISIONS:
   Ver        Date           Author                         Description
   ---------  ----------     ---------------                --------------------------
   1.0       19/10/2020     a.mejiasc@alumnos.duoc.cl       1. Procedimiento Listar 
***************************************************************************************************************/
    p_estado := 0;
    p_glosa := 'PKG_PRODUCTO.SP_Listar ha sido ejecutado correctamente';

    OPEN pc_productos FOR  
    SELECT  IDPRODUCTO,
            CODIGOPRODUCTO,
            NOMBRE,
            DESCRIPCION,
            STOCK,
            VALORUNITARIO,
            TIPOVENTA,
            CALIDAD,
            FECHACADUCIDAD,
            FECHACREACION,
            FECHAMODIFICACION,
            IDCATEGORIA,
            IDUSUARIO
    FROM Producto WHERE IdUsuario = p_IdProductor
    ORDER BY IdProducto DESC;

    EXCEPTION
    when no_data_found THEN
         p_estado := 1;
         p_glosa := 'No hay datos de producto';
    WHEN OTHERS THEN
      p_estado := -1;
      p_glosa := 'ERROR SQL '|| SQLCODE || ' --> ' || SUBSTR(SQLERRM, 1, 255);

END SP_Listar;
END "PKG_PRODUCTO";
