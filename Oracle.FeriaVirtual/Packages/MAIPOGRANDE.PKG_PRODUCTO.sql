-- ****** Objeto: Paquete MAIPOGRANDE.PKG_PRODUCTO Fecha de Script: 26-10-2020 16:22:17 ******
CREATE OR REPLACE PACKAGE "MAIPOGRANDE"."PKG_PRODUCTO" AS
PROCEDURE SP_Crear( p_CODIGOPRODUCTO IN VARCHAR2, 
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
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER);
PROCEDURE SP_Modificar( p_IdProducto IN INTEGER,
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
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER);
PROCEDURE SP_Listar(p_IdProductor IN INTEGER,
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER, pc_productos OUT sys_refcursor);                    
END "PKG_PRODUCTO";
