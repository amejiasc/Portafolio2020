-- ****** Objeto: Cuerpo del Paquete MAIPOGRANDE.PKG_PRODUCTO.BODY Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE BODY "MAIPOGRANDE"."PKG_PRODUCTO" AS
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
