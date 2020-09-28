-- ****** Objeto: Tabla MAIPOGRANDE.ORDEN Fecha de Script: 28-09-2020 16:13:30 ******
  CREATE TABLE "MAIPOGRANDE"."ORDEN" 
   (	"IDORDEN" NUMBER(*,0) NOT NULL ENABLE,
	"FECHAORDEN" DATE NOT NULL ENABLE,
	"FIRMACONTRATO" CHAR(1) NOT NULL ENABLE,
	"FECHAFIRMACONTRATO" DATE,
	"IDUSUARIO" NUMBER(*,0) NOT NULL ENABLE,
	"PRECIOVENTA" NUMBER(*,0) NOT NULL ENABLE,
	"IDUSUARIO2" NUMBER(*,0) NOT NULL ENABLE,
	"ESTADO" VARCHAR2(20) NOT NULL ENABLE,
	CONSTRAINT "ORDEN_PK" PRIMARY KEY ("IDORDEN") ENABLE,
	CONSTRAINT "ORDEN_CLIENTEEXTERNO_FK" FOREIGN KEY ("IDUSUARIO")
	 REFERENCES "MAIPOGRANDE"."CLIENTEEXTERNO" ("IDUSUARIO") ENABLE,
	CONSTRAINT "ORDEN_CLIENTEINTERNO_FK" FOREIGN KEY ("IDUSUARIO2")
	 REFERENCES "MAIPOGRANDE"."CLIENTEINTERNO" ("IDUSUARIO") ENABLE
   );