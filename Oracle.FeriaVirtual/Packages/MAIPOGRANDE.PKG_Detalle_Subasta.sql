-- ****** Objeto: Paquete MAIPOGRANDE.PKG_Detalle_Subasta Fecha de Script: 19-10-2020 23:07:01 ******
CREATE OR REPLACE PACKAGE "MAIPOGRANDE"."PKG_Detalle_Subasta" AS
PROCEDURE SP_Crear_DetalleSubasta( p_fecha IN detallesubasta.fecha%TYPE, p_Estado_detalle_subasta IN VARCHAR2, p_idSubasta in number, p_idUsuario in number,p_Monto IN detallesubasta.montooferta%TYPE,p_IdDetalle_Subasta out number, p_glosa OUT VARCHAR2, p_estado OUT INTEGER);
PROCEDURE SP_Listar_DetalleSubasta(p_IdDetalle_Subasta IN NUMBER,OUT_PC_GET_DetalleSubasta OUT SYS_REFCURSOR,p_estado out number, p_glosa out VARCHAR2);

PROCEDURE SP_Modificar_DetalleSubasta(p_IdDetalle_Subasta IN number, p_fecha IN detallesubasta.fecha%TYPE, p_Estado_detalle_subasta IN VARCHAR2, p_idSubasta in number, p_idUsuario in number,p_Monto IN detallesubasta.montooferta%TYPE, p_glosa OUT VARCHAR2, p_estado OUT INTEGER);

PROCEDURE SP_Eliminar_DetalleSubasta(p_IdDetalle_Subasta IN number, p_glosa OUT VARCHAR2, p_estado OUT number);

end "PKG_Detalle_Subasta";
