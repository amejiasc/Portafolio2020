-- ****** Objeto: Paquete MAIPOGRANDE.PKG_PRODUCTO Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE "MAIPOGRANDE"."PKG_PRODUCTO" AS
PROCEDURE SP_Listar(p_IdProductor IN INTEGER,
                    p_glosa OUT VARCHAR2, p_estado OUT INTEGER, pc_productos OUT sys_refcursor);
END "PKG_PRODUCTO";
