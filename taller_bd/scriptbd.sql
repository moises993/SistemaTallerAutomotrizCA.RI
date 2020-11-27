PGDMP     /                
    x            BD_TallerAutomotrizCA.RI    13.0 (Debian 13.0-1.pgdg100+1)    13.0 �    B           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            C           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            D           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            E           1262    16384    BD_TallerAutomotrizCA.RI    DATABASE     n   CREATE DATABASE "BD_TallerAutomotrizCA.RI" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'en_US.utf8';
 *   DROP DATABASE "BD_TallerAutomotrizCA.RI";
                postgres    false                        2615    16385    Taller    SCHEMA        CREATE SCHEMA "Taller";
    DROP SCHEMA "Taller";
                postgres    false            F           0    0    SCHEMA "Taller"    COMMENT     n   COMMENT ON SCHEMA "Taller" IS 'Esquema creado para las tablas, vistas y funciones/procedimientos del taller';
                   postgres    false    6            $           1255    40963 ^   bcrRegistrarAccion(character varying, character varying, character varying, character varying)    FUNCTION     _  CREATE FUNCTION "Taller"."bcrRegistrarAccion"(pusuario character varying, ptabla character varying, pcontrolador character varying, paccion character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
BEGIN
	INSERT INTO "Taller"."Bitacora" VALUES
	(
		DEFAULT, 
		"pusuario",
		"ptabla",
		"pcontrolador",
		"paccion",
		LOCALTIMESTAMP
	);
END;
$$;
 �   DROP FUNCTION "Taller"."bcrRegistrarAccion"(pusuario character varying, ptabla character varying, pcontrolador character varying, paccion character varying);
       Taller          postgres    false    6            �            1255    16386 d   cltCrearCliente(character varying, character varying, character varying, character varying, boolean) 	   PROCEDURE     �  CREATE PROCEDURE "Taller"."cltCrearCliente"(nmb character varying DEFAULT 'pendiente'::character varying, ap1 character varying DEFAULT 'pendiente'::character varying, ap2 character varying DEFAULT 'pendiente'::character varying, ced character varying DEFAULT 'pendiente'::character varying, frec boolean DEFAULT false)
    LANGUAGE plpgsql
    AS $$
BEGIN
	INSERT INTO "Taller"."Cliente" VALUES
	(
		DEFAULT, nmb, ap1, ap2, ced, frec, CURRENT_DATE
	);
END;
$$;
 �   DROP PROCEDURE "Taller"."cltCrearCliente"(nmb character varying, ap1 character varying, ap2 character varying, ced character varying, frec boolean);
       Taller          postgres    false    6            �            1255    16387 p   cltEditarCliente(character varying, character varying, character varying, character varying, character, boolean) 	   PROCEDURE     {  CREATE PROCEDURE "Taller"."cltEditarCliente"(id character varying, nmb character varying, ap1 character varying, ap2 character varying, ced character, frec boolean)
    LANGUAGE plpgsql
    AS $$
BEGIN
	UPDATE "Taller"."Cliente" SET
	"nombre"="nmb", 
	"pmrApellido"="ap1",
	"sgndApellido"="ap2",
	"cedula"="ced",
	"cltFrecuente"="frec"
	WHERE "Cliente"."cedula" = "id";
END;
$$;
 �   DROP PROCEDURE "Taller"."cltEditarCliente"(id character varying, nmb character varying, ap1 character varying, ap2 character varying, ced character, frec boolean);
       Taller          postgres    false    6                       1255    16388 %   cltEliminarCliente(character varying) 	   PROCEDURE     �  CREATE PROCEDURE "Taller"."cltEliminarCliente"(identificacion character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE 
	id_cliente INTEGER;
BEGIN
	SELECT "Cliente"."IDCliente" FROM "Taller"."Cliente"
	INTO id_cliente
	WHERE "Cliente"."cedula" = "identificacion";
	DELETE FROM "Taller"."DetallesCliente" WHERE 
	"DetallesCliente"."IDCliente" = id_cliente;
	DELETE FROM "Taller"."Cliente" WHERE "Cliente"."cedula"="identificacion";
END;
$$;
 P   DROP PROCEDURE "Taller"."cltEliminarCliente"(identificacion character varying);
       Taller          postgres    false    6            �            1255    16389    cltValidarCedula(character)    FUNCTION     )  CREATE FUNCTION "Taller"."cltValidarCedula"(pcedula character) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE
	k INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."Cliente" 
	INTO k
	WHERE "Cliente"."cedula" = "pcedula";
	
	IF k = 1 THEN
		RETURN FALSE;
	ELSE 
		RETURN TRUE;
	END IF;
END
$$;
 >   DROP FUNCTION "Taller"."cltValidarCedula"(pcedula character);
       Taller          postgres    false    6            �            1259    16390    Cliente    TABLE     A  CREATE TABLE "Taller"."Cliente" (
    "IDCliente" integer NOT NULL,
    nombre character varying(150) NOT NULL,
    "pmrApellido" character varying(50) NOT NULL,
    "sgndApellido" character varying(50) NOT NULL,
    cedula character(30) NOT NULL,
    "cltFrecuente" boolean NOT NULL,
    "fechaIngreso" date NOT NULL
);
    DROP TABLE "Taller"."Cliente";
       Taller         heap    postgres    false    6            G           0    0    TABLE "Cliente"    COMMENT     G   COMMENT ON TABLE "Taller"."Cliente" IS 'Información de los clientes';
          Taller          postgres    false    201            �            1255    16393 )   cltVerClientePorCedula(character varying)    FUNCTION     �   CREATE FUNCTION "Taller"."cltVerClientePorCedula"(pcedula character varying) RETURNS SETOF "Taller"."Cliente"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Cliente"
	WHERE "Cliente"."cedula" = "pcedula";
END;
$$;
 L   DROP FUNCTION "Taller"."cltVerClientePorCedula"(pcedula character varying);
       Taller          postgres    false    6    201            �            1255    16394    cltVerClientes()    FUNCTION     �   CREATE FUNCTION "Taller"."cltVerClientes"() RETURNS SETOF "Taller"."Cliente"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	EXECUTE 'SELECT * FROM "Taller"."Cliente";';
END;
$$;
 +   DROP FUNCTION "Taller"."cltVerClientes"();
       Taller          postgres    false    6    201            �            1255    16408 Z   ctaActualizarCita(integer, date, character, character varying, character varying, boolean) 	   PROCEDURE       CREATE PROCEDURE "Taller"."ctaActualizarCita"(pid integer, pfecha date, phora character, pasunto character varying, pdesc character varying, pconf boolean)
    LANGUAGE plpgsql
    AS $$
BEGIN
	UPDATE "Taller"."Cita" SET
		"fecha" = "pfecha",
		"hora" = "phora",
		"asunto" = "pasunto",
		"descripcion" = "pdesc",
		"citaConfirmada" = "pconf"
	WHERE "Cita"."IDCita" = "pid";
END
$$;
 �   DROP PROCEDURE "Taller"."ctaActualizarCita"(pid integer, pfecha date, phora character, pasunto character varying, pdesc character varying, pconf boolean);
       Taller          postgres    false    6                       1255    16409 `   ctaCrearCita(character, integer, date, character, character varying, character varying, boolean) 	   PROCEDURE     @  CREATE PROCEDURE "Taller"."ctaCrearCita"(pced character, pidtec integer, pfecha date, phora character, pasunto character varying, pdesc character varying, pconf boolean)
    LANGUAGE plpgsql
    AS $$
DECLARE
	byteTec INTEGER;
	byteClt INTEGER;
	idRegClt INTEGER;
	idRegTec INTEGER;
	idRegVhl INTEGER;
	k INTEGER;
	n INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."Tecnico"
	INTO byteTec
	WHERE "Tecnico"."IDTecnico" = "pidtec";
	
	SELECT COUNT(*) FROM "Taller"."Cliente"
	INTO byteClt 
	WHERE "Cliente"."cedula" = "pced";
	
	IF byteTec = 1 AND byteClt = 1 THEN
		SELECT "Cliente"."IDCliente" FROM "Taller"."Cliente"
		INTO idRegClt
		WHERE "Cliente"."cedula" = "pced";
		
		SELECT "Tecnico"."IDTecnico" FROM "Taller"."Tecnico"
		INTO idRegTec
		WHERE "Tecnico"."IDTecnico" = "pidtec";
		
		SELECT "Servicio"."IDVehiculo" FROM 
		"Taller"."Servicio",
		"Taller"."Vehiculo",
		"Taller"."Cliente"
		INTO idRegVhl
		WHERE "Servicio"."IDVehiculo" = "Vehiculo"."IDVehiculo" AND
		"Vehiculo"."IDCliente" = "Cliente"."IDCliente" AND
		"Cliente"."IDCliente" = idRegClt AND 
		"Servicio"."descripcion" LIKE "pdesc" || '%';
		
		SELECT "Cita"."IDCita" FROM "Taller"."Cita" 
		INTO n
		WHERE "Cita"."fecha" = "pfecha" AND
		"Cita"."hora" = "phora" AND
		"Cita"."IDTecnico" = idRegTec;
		GET DIAGNOSTICS k = ROW_COUNT;
		
		IF k = 0 THEN
			INSERT INTO "Taller"."Cita"
			VALUES
			(
				DEFAULT, idRegTec, "pced", "pfecha", "phora",
				"pasunto", "pdesc", "pconf", idRegClt
			);
		END IF;
		
		IF "pconf" = TRUE THEN
			PERFORM "Taller"."odnCrearOrdenFn"(idRegVhl, "pdesc", "pced");
		END IF;
	END IF;
END
$$;
 �   DROP PROCEDURE "Taller"."ctaCrearCita"(pced character, pidtec integer, pfecha date, phora character, pasunto character varying, pdesc character varying, pconf boolean);
       Taller          postgres    false    6            �            1255    16410    ctaEliminarCita(integer) 	   PROCEDURE     �   CREATE PROCEDURE "Taller"."ctaEliminarCita"(pid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
	DELETE FROM "Taller"."Cita"
	WHERE "Cita"."IDCita" = "pid";
END
$$;
 8   DROP PROCEDURE "Taller"."ctaEliminarCita"(pid integer);
       Taller          postgres    false    6            !           1255    40962 \   ctaFnActualizarCita(integer, date, character, character varying, character varying, boolean)    FUNCTION     ~  CREATE FUNCTION "Taller"."ctaFnActualizarCita"(pid integer, pfecha date, phora character, pasunto character varying, pdesc character varying, pconf boolean) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE
	n INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."Cita" 
	INTO n
	WHERE "Cita"."IDCita" <> "pid"
    AND "Cita"."fecha" = "pfecha" 
	AND "Cita"."hora" = "phora";
		
	IF n > 0 THEN
		RETURN FALSE;
	
	ELSE
		UPDATE "Taller"."Cita" SET
			"fecha" = "pfecha",
			"hora" = "phora",
			"asunto" = "pasunto",
			"descripcion" = "pdesc",
			"citaConfirmada" = "pconf"
		WHERE "Cita"."IDCita" = "pid";
		RETURN TRUE;
	END IF;
END;
$$;
 �   DROP FUNCTION "Taller"."ctaFnActualizarCita"(pid integer, pfecha date, phora character, pasunto character varying, pdesc character varying, pconf boolean);
       Taller          postgres    false    6            "           1255    32793 b   ctaFnCrearCita(character, integer, date, character, character varying, character varying, boolean)    FUNCTION     y  CREATE FUNCTION "Taller"."ctaFnCrearCita"(pced character, pidtec integer, pfecha date, phora character, pasunto character varying, pdesc character varying, pconf boolean) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE
	byteTec INTEGER;
	byteClt INTEGER;
	idRegClt INTEGER;
	idRegTec INTEGER;
	idRegVhl INTEGER;
	k INTEGER;
	n INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."Tecnico"
	INTO byteTec
	WHERE "Tecnico"."IDTecnico" = "pidtec";
	
	SELECT COUNT(*) FROM "Taller"."Cliente"
	INTO byteClt 
	WHERE "Cliente"."cedula" = "pced";
	
	IF byteTec = 1 AND byteClt = 1 THEN
		SELECT "Cliente"."IDCliente" FROM "Taller"."Cliente"
		INTO idRegClt
		WHERE "Cliente"."cedula" = "pced";
		
		SELECT "Tecnico"."IDTecnico" FROM "Taller"."Tecnico"
		INTO idRegTec
		WHERE "Tecnico"."IDTecnico" = "pidtec";
		
		SELECT "Servicio"."IDVehiculo" FROM 
		"Taller"."Servicio",
		"Taller"."Vehiculo",
		"Taller"."Cliente"
		INTO idRegVhl
		WHERE "Servicio"."IDVehiculo" = "Vehiculo"."IDVehiculo" AND
		"Vehiculo"."IDCliente" = "Cliente"."IDCliente" AND
		"Cliente"."IDCliente" = idRegClt AND 
		"Servicio"."descripcion" LIKE "pdesc" || '%';
		
		SELECT "Cita"."IDCita" FROM "Taller"."Cita" 
		INTO n
		WHERE "Cita"."fecha" = "pfecha" AND
		"Cita"."hora" = "phora" AND
		"Cita"."IDTecnico" = "pidtec";
		GET DIAGNOSTICS k = ROW_COUNT;
		
		IF k > 0 THEN
			RETURN FALSE;
		ELSE
			INSERT INTO "Taller"."Cita"
			VALUES
			(
				DEFAULT, idRegTec, "pced", "pfecha", "phora",
				"pasunto", "pdesc", "pconf", idRegClt
			);
			RETURN TRUE;
		END IF;
		
		IF "pconf" = TRUE THEN
			PERFORM "Taller"."odnCrearOrdenFn"(idRegVhl, "pdesc", "pced");
		END IF;
	END IF;
END
$$;
 �   DROP FUNCTION "Taller"."ctaFnCrearCita"(pced character, pidtec integer, pfecha date, phora character, pasunto character varying, pdesc character varying, pconf boolean);
       Taller          postgres    false    6            �            1255    16411    ctaVerCedulasTecnico()    FUNCTION     �   CREATE FUNCTION "Taller"."ctaVerCedulasTecnico"() RETURNS TABLE(cedula character)
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY 
	SELECT "Tecnico"."cedula" FROM "Taller"."Tecnico";
END; $$;
 1   DROP FUNCTION "Taller"."ctaVerCedulasTecnico"();
       Taller          postgres    false    6            �            1259    16395    Cita    TABLE     �  CREATE TABLE "Taller"."Cita" (
    "IDCita" integer NOT NULL,
    "IDTecnico" integer NOT NULL,
    "cedulaCliente" character(30) NOT NULL,
    fecha date NOT NULL,
    hora character(10) DEFAULT 'sin definir'::bpchar NOT NULL,
    asunto character varying(100) NOT NULL,
    descripcion character varying DEFAULT 'sin definir'::character varying NOT NULL,
    "citaConfirmada" boolean NOT NULL,
    "IDCliente" integer NOT NULL
);
    DROP TABLE "Taller"."Cita";
       Taller         heap    postgres    false    6            H           0    0    TABLE "Cita"    COMMENT     K   COMMENT ON TABLE "Taller"."Cita" IS 'Información de las citas generadas';
          Taller          postgres    false    202            �            1255    16412    ctaVerCitaPorId(integer)    FUNCTION     �   CREATE FUNCTION "Taller"."ctaVerCitaPorId"(pid integer) RETURNS SETOF "Taller"."Cita"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY 
	SELECT * FROM "Taller"."Cita"
	WHERE "Cita"."IDCita" = "pid";
END
$$;
 7   DROP FUNCTION "Taller"."ctaVerCitaPorId"(pid integer);
       Taller          postgres    false    202    6            �            1255    16413    ctaVerCitas()    FUNCTION     �   CREATE FUNCTION "Taller"."ctaVerCitas"() RETURNS SETOF "Taller"."Cita"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Cita";
END
$$;
 (   DROP FUNCTION "Taller"."ctaVerCitas"();
       Taller          postgres    false    6    202                       1255    16414 G   dtcActualizarDetalles(integer, character varying, character, character) 	   PROCEDURE     U  CREATE PROCEDURE "Taller"."dtcActualizarDetalles"(pid integer, pdireccion character varying, ptelefono character, pcorreo character)
    LANGUAGE plpgsql
    AS $$
BEGIN
	UPDATE "Taller"."DetallesCliente" SET
		"direccion" = "pdireccion",
		"telefono" = "ptelefono",
		"correo" = "pcorreo"
	WHERE "DetallesCliente"."codDet" = "pid";
END
$$;
 �   DROP PROCEDURE "Taller"."dtcActualizarDetalles"(pid integer, pdireccion character varying, ptelefono character, pcorreo character);
       Taller          postgres    false    6            �            1255    16618    dtcEliminarDetalle(integer) 	   PROCEDURE     �   CREATE PROCEDURE "Taller"."dtcEliminarDetalle"(pid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
	DELETE FROM "Taller"."DetallesCliente"
	WHERE "DetallesCliente"."codDet" = "pid";
END
$$;
 ;   DROP PROCEDURE "Taller"."dtcEliminarDetalle"(pid integer);
       Taller          postgres    false    6            (           1255    40971 R   dtcFnActualizarDetalles(integer, character varying, character, character, integer)    FUNCTION     �  CREATE FUNCTION "Taller"."dtcFnActualizarDetalles"(pidcliente integer, pdireccion character varying, ptelefono character, pcorreo character, pid integer) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE
	CONTEO_EXISTENTE INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."DetallesCliente"
	INTO CONTEO_EXISTENTE
	WHERE "DetallesCliente"."IDCliente" <> "pidcliente"
	AND "DetallesCliente"."telefono" = "ptelefono"
	AND "DetallesCliente"."correo" = "pcorreo";
	
	IF CONTEO_EXISTENTE > 0 THEN
		RETURN FALSE;
	ELSE 
		UPDATE "Taller"."DetallesCliente" SET
		"direccion" = "pdireccion",
		"telefono" = "ptelefono",
		"correo" = "pcorreo"
		WHERE "DetallesCliente"."codDet" = "pid";
		RETURN TRUE;
	END IF;
END
$$;
 �   DROP FUNCTION "Taller"."dtcFnActualizarDetalles"(pidcliente integer, pdireccion character varying, ptelefono character, pcorreo character, pid integer);
       Taller          postgres    false    6            &           1255    40969 F   dtcFnIngresarDetalle(integer, character varying, character, character)    FUNCTION     B  CREATE FUNCTION "Taller"."dtcFnIngresarDetalle"(pid integer, pdireccion character varying, ptelefono character, pcorreo character) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE
	CONTEO_EXISTENTE INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."DetallesCliente"
	INTO CONTEO_EXISTENTE
	WHERE "DetallesCliente"."telefono" = "ptelefono"
	AND "DetallesCliente"."correo" = "pcorreo";
	
	IF CONTEO_EXISTENTE > 0 THEN
		RETURN FALSE;
	ELSE
		INSERT INTO "Taller"."DetallesCliente" 
		VALUES("pid", "pdireccion", "ptelefono", "pcorreo", DEFAULT);
		RETURN TRUE;
	END IF;
END
$$;
 �   DROP FUNCTION "Taller"."dtcFnIngresarDetalle"(pid integer, pdireccion character varying, ptelefono character, pcorreo character);
       Taller          postgres    false    6                       1255    16416 E   dtcIngresarDetalles(integer, character varying, character, character) 	   PROCEDURE     �  CREATE PROCEDURE "Taller"."dtcIngresarDetalles"(pid integer, pdireccion character varying, ptelefono character, pcorreo character)
    LANGUAGE plpgsql
    AS $$
DECLARE
	k INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."Cliente"
	INTO k
	WHERE "Cliente"."IDCliente" = "pid";
	
	IF k = 1 THEN
		INSERT INTO "Taller"."DetallesCliente" 
		VALUES("pid", "pdireccion", "ptelefono", "pcorreo", DEFAULT);
	END IF;
END
$$;
 �   DROP PROCEDURE "Taller"."dtcIngresarDetalles"(pid integer, pdireccion character varying, ptelefono character, pcorreo character);
       Taller          postgres    false    6            �            1259    16417    DetallesCliente    TABLE     �   CREATE TABLE "Taller"."DetallesCliente" (
    "IDCliente" integer NOT NULL,
    direccion character varying NOT NULL,
    telefono character(25) NOT NULL,
    correo character(35) NOT NULL,
    "codDet" integer NOT NULL
);
 '   DROP TABLE "Taller"."DetallesCliente";
       Taller         heap    postgres    false    6            I           0    0    TABLE "DetallesCliente"    COMMENT     ^   COMMENT ON TABLE "Taller"."DetallesCliente" IS 'Separación de los detalles de cada cliente';
          Taller          postgres    false    204                       1255    16592    dtcVerDetalleCliente(character)    FUNCTION     z  CREATE FUNCTION "Taller"."dtcVerDetalleCliente"(pcedula character) RETURNS SETOF "Taller"."DetallesCliente"
    LANGUAGE plpgsql
    AS $$
DECLARE
	id INTEGER;
BEGIN
	SELECT "Cliente"."IDCliente" INTO id
	FROM "Taller"."Cliente"
	WHERE "Cliente"."cedula" = "pcedula";
	RETURN QUERY
	SELECT * FROM "Taller"."DetallesCliente"
	WHERE "DetallesCliente"."IDCliente" = "id";
END;
$$;
 B   DROP FUNCTION "Taller"."dtcVerDetalleCliente"(pcedula character);
       Taller          postgres    false    6    204                       1255    16619    dtcVerDetallePorId(integer)    FUNCTION     �   CREATE FUNCTION "Taller"."dtcVerDetallePorId"(pid integer) RETURNS SETOF "Taller"."DetallesCliente"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY 
	SELECT * FROM "Taller"."DetallesCliente"
	WHERE "DetallesCliente"."codDet" = "pid";
END
$$;
 :   DROP FUNCTION "Taller"."dtcVerDetallePorId"(pid integer);
       Taller          postgres    false    204    6                       1255    16424 G   dttActualizarDetalles(integer, character varying, character, character) 	   PROCEDURE     U  CREATE PROCEDURE "Taller"."dttActualizarDetalles"(pid integer, pdireccion character varying, ptelefono character, pcorreo character)
    LANGUAGE plpgsql
    AS $$
BEGIN
	UPDATE "Taller"."DetallesTecnico" SET
		"direccion" = "pdireccion",
		"telefono" = "ptelefono",
		"correo" = "pcorreo"
	WHERE "DetallesTecnico"."codDet" = "pid";
END
$$;
 �   DROP PROCEDURE "Taller"."dttActualizarDetalles"(pid integer, pdireccion character varying, ptelefono character, pcorreo character);
       Taller          postgres    false    6                       1255    16425    dttEliminarDetalle(integer) 	   PROCEDURE     �   CREATE PROCEDURE "Taller"."dttEliminarDetalle"(pid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
	DELETE FROM "Taller"."DetallesTecnico"
	WHERE "DetallesTecnico"."codDet" = "pid";
END
$$;
 ;   DROP PROCEDURE "Taller"."dttEliminarDetalle"(pid integer);
       Taller          postgres    false    6            )           1255    40972 R   dttFnActualizarDetalles(integer, character varying, character, character, integer)    FUNCTION     �  CREATE FUNCTION "Taller"."dttFnActualizarDetalles"(pidtecnico integer, pdireccion character varying, ptelefono character, pcorreo character, pid integer) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE
	CONTEO_EXISTENTE INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."DetallesTecnico"
	INTO CONTEO_EXISTENTE
	WHERE "DetallesTecnico"."IDTecnico" <> "pidtecnico"
	AND "DetallesTecnico"."telefono" = "ptelefono"
	AND "DetallesTecnico"."correo" = "pcorreo";
	
	IF CONTEO_EXISTENTE > 0 THEN
		RETURN FALSE;
	ELSE 
		UPDATE "Taller"."DetallesTecnico" SET
		"direccion" = "pdireccion",
		"telefono" = "ptelefono",
		"correo" = "pcorreo"
		WHERE "DetallesTecnico"."codDet" = "pid";
		RETURN TRUE;
	END IF;
END
$$;
 �   DROP FUNCTION "Taller"."dttFnActualizarDetalles"(pidtecnico integer, pdireccion character varying, ptelefono character, pcorreo character, pid integer);
       Taller          postgres    false    6            '           1255    40970 F   dttFnIngresarDetalle(integer, character varying, character, character)    FUNCTION     B  CREATE FUNCTION "Taller"."dttFnIngresarDetalle"(pid integer, pdireccion character varying, ptelefono character, pcorreo character) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE
	CONTEO_EXISTENTE INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."DetallesTecnico"
	INTO CONTEO_EXISTENTE
	WHERE "DetallesTecnico"."telefono" = "ptelefono"
	AND "DetallesTecnico"."correo" = "pcorreo";
	
	IF CONTEO_EXISTENTE > 0 THEN
		RETURN FALSE;
	ELSE
		INSERT INTO "Taller"."DetallesTecnico" 
		VALUES("pid", "pdireccion", "ptelefono", "pcorreo", DEFAULT);
		RETURN TRUE;
	END IF;
END
$$;
 �   DROP FUNCTION "Taller"."dttFnIngresarDetalle"(pid integer, pdireccion character varying, ptelefono character, pcorreo character);
       Taller          postgres    false    6                       1255    16426 E   dttIngresarDetalles(integer, character varying, character, character) 	   PROCEDURE     �  CREATE PROCEDURE "Taller"."dttIngresarDetalles"(pid integer, pdireccion character varying, ptelefono character, pcorreo character)
    LANGUAGE plpgsql
    AS $$
DECLARE
	k INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."Tecnico"
	INTO k
	WHERE "Tecnico"."IDTecnico" = "pid";
	
	IF k = 1 THEN
		INSERT INTO "Taller"."DetallesTecnico" 
		VALUES("pid", "pdireccion", "ptelefono", "pcorreo", DEFAULT);
	END IF;
END
$$;
 �   DROP PROCEDURE "Taller"."dttIngresarDetalles"(pid integer, pdireccion character varying, ptelefono character, pcorreo character);
       Taller          postgres    false    6            �            1259    16427    DetallesTecnico    TABLE     �   CREATE TABLE "Taller"."DetallesTecnico" (
    "IDTecnico" integer NOT NULL,
    direccion character varying NOT NULL,
    telefono character(25) NOT NULL,
    correo character(35) NOT NULL,
    "codDet" integer NOT NULL
);
 '   DROP TABLE "Taller"."DetallesTecnico";
       Taller         heap    postgres    false    6            J           0    0    TABLE "DetallesTecnico"    COMMENT     K   COMMENT ON TABLE "Taller"."DetallesTecnico" IS 'Detalles de cada cliente';
          Taller          postgres    false    205                       1255    16433    dttVerDetallePorId(integer)    FUNCTION     �   CREATE FUNCTION "Taller"."dttVerDetallePorId"(pid integer) RETURNS SETOF "Taller"."DetallesTecnico"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."DetallesTecnico"
	WHERE "DetallesTecnico"."codDet" = "pid";
END;
$$;
 :   DROP FUNCTION "Taller"."dttVerDetallePorId"(pid integer);
       Taller          postgres    false    205    6                       1255    16621    dttVerDetalleTecnico(character)    FUNCTION     z  CREATE FUNCTION "Taller"."dttVerDetalleTecnico"(pcedula character) RETURNS SETOF "Taller"."DetallesTecnico"
    LANGUAGE plpgsql
    AS $$
DECLARE
	id INTEGER;
BEGIN
	SELECT "Tecnico"."IDTecnico" INTO id
	FROM "Taller"."Tecnico"
	WHERE "Tecnico"."cedula" = "pcedula";
	RETURN QUERY
	SELECT * FROM "Taller"."DetallesTecnico"
	WHERE "DetallesTecnico"."IDTecnico" = "id";
END;
$$;
 B   DROP FUNCTION "Taller"."dttVerDetalleTecnico"(pcedula character);
       Taller          postgres    false    205    6                       1255    16434 ,   expActualizarExp(integer, character varying) 	   PROCEDURE     �   CREATE PROCEDURE "Taller"."expActualizarExp"(pid integer, pdesc character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
	UPDATE "Taller"."Expediente" SET
		"Descripcion" = "pdesc"
	WHERE "Expediente"."IDExpediente" = "pid";
END;
$$;
 R   DROP PROCEDURE "Taller"."expActualizarExp"(pid integer, pdesc character varying);
       Taller          postgres    false    6            #           1255    16435    expCrearExpediente(integer) 	   PROCEDURE     �  CREATE PROCEDURE "Taller"."expCrearExpediente"(pidv integer)
    LANGUAGE plpgsql
    AS $$
DECLARE
	nmbTec VARCHAR;
	ap1Tec VARCHAR;
	ap2Tec VARCHAR;
	nmbCpltTec VARCHAR;
	nmbClt VARCHAR;
	ap1Clt VARCHAR;
	ap2Clt VARCHAR;
	nmbCpltClt VARCHAR;
	asunto VARCHAR;
	descr VARCHAR;
	mcr VARCHAR;
	mdl INTEGER;
	plc VARCHAR;
	buscVhl INTEGER;
	buscClt INTEGER;
	buscTec INTEGER;
	buscCta INTEGER;
	buscSvc INTEGER;
	k INTEGER;
	REGISTRO_PREVIO VARCHAR;
	REGISTRO_NUEVO VARCHAR;
BEGIN
	SELECT "Vehiculo"."marca", "Vehiculo"."modelo", "Vehiculo"."placa"
	FROM "Taller"."Vehiculo" INTO
	mcr, mdl, plc
	WHERE "Vehiculo"."IDVehiculo" = "pidv";
	GET DIAGNOSTICS buscVhl = ROW_COUNT;
	
	SELECT "Cliente"."nombre", "Cliente"."pmrApellido", "Cliente"."sgndApellido"
	FROM "Taller"."Cliente", "Taller"."Vehiculo" INTO
	nmbClt, ap1Clt, ap2Clt
	WHERE "Vehiculo"."IDVehiculo" = "pidv" AND "Vehiculo"."IDCliente" = "Cliente"."IDCliente";
	GET DIAGNOSTICS buscClt = ROW_COUNT;
	
	SELECT CONCAT(nmbClt, ' ', ap1Clt, ' ', ap2Clt) INTO nmbCpltClt;

	SELECT "Tecnico"."nombre", "Tecnico"."pmrApellido", "Tecnico"."sgndApellido"
	FROM "Taller"."Tecnico", "Taller"."Vehiculo", "Taller"."Cliente", "Taller"."Cita"
	INTO nmbTec, ap1Tec, ap2Tec
	WHERE "Vehiculo"."IDVehiculo" = "pidv" AND "Vehiculo"."IDCliente" = "Cliente"."IDCliente"
	AND "Cliente"."IDCliente" = "Cita"."IDCliente" 
	AND "Cita"."IDTecnico" = "Tecnico"."IDTecnico";
	GET DIAGNOSTICS buscTec = ROW_COUNT;
	
	SELECT CONCAT(nmbTec, ' ', ap1Tec, ' ', ap2Tec) INTO nmbCpltTec;
	
	SELECT "Cita"."asunto" FROM
	"Taller"."Cita", "Taller"."Cliente", "Taller"."Vehiculo"
	INTO asunto
	WHERE "Vehiculo"."IDVehiculo" = "pidv" 
	AND "Vehiculo"."IDCliente" = "Cliente"."IDCliente"
	AND "Cita"."IDCliente" = "Cliente"."IDCliente";
	GET DIAGNOSTICS buscCta = ROW_COUNT;
	
	SELECT STRING_AGG
	(	
		'Fecha: ' || ' ' || "Cita"."fecha" || ' ' || E'\n'
		'Hora: ' || ' ' || "Cita"."hora" || ' ' || E'\n'
		'Técnico: ' || "Tecnico"."nombre" || ' ' || "Tecnico"."pmrApellido" || ' ' || "Tecnico"."sgndApellido" || ' ' || E'\n'
		'Servicio: ' || "Cita"."descripcion", E'\n\n'
	)
	FROM "Taller"."Cita", "Taller"."Cliente",
	"Taller"."Vehiculo", "Taller"."Tecnico"
	INTO descr
	WHERE "Cita"."IDTecnico" = "Tecnico"."IDTecnico"
	AND "Cita"."IDCliente" = "Cliente"."IDCliente" 
	AND "Cliente"."IDCliente" = "Vehiculo"."IDCliente"
	AND "Vehiculo"."IDVehiculo" = "pidv"
	AND "Cita"."citaConfirmada" = TRUE;
	
	/*SELECT STRING_AGG("Servicio"."descripcion", E'\n')
	FROM "Taller"."Servicio"
	INTO descr
	WHERE "Servicio"."IDVehiculo" = "pidv";
	GET DIAGNOSTICS buscSvc = ROW_COUNT;
	AND buscSvc > 0
	*/
	
	SELECT COUNT(*) FROM "Taller"."Expediente" 
	INTO k
	WHERE "Expediente"."IDVehiculo" = "pidv";
	
	IF buscVhl = 1 AND buscClt = 1 AND buscTec = 1 AND buscCta = 1 AND k = 0 THEN
		INSERT INTO "Taller"."Expediente" VALUES
		(
			DEFAULT, "pidv", nmbCpltTec, asunto, descr, CURRENT_DATE, nmbCpltClt, mcr, mdl, plc
		);
	END IF;
	
	IF k = 1 THEN
		/*SELECT "Expediente"."Descripcion" 
		FROM "Taller"."Expediente"
		INTO REGISTRO_PREVIO
		WHERE "Expediente"."IDVehiculo" = "pidv";
		
		SELECT STRING_AGG
		(
			REGISTRO_PREVIO || E'\n' ||
			descr, E'\n'
		)
		INTO REGISTRO_NUEVO;*/

		UPDATE "Taller"."Expediente" SET
		"Descripcion" = descr 
		WHERE "Expediente"."IDVehiculo" = "pidv"; 
	END IF;
END;
$$;
 <   DROP PROCEDURE "Taller"."expCrearExpediente"(pidv integer);
       Taller          postgres    false    6            �            1255    16436    expEliminarExp(integer) 	   PROCEDURE     �   CREATE PROCEDURE "Taller"."expEliminarExp"(pid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
	DELETE FROM "Taller"."Expediente"
	WHERE "Expediente"."IDExpediente" = "pid";
END;
$$;
 7   DROP PROCEDURE "Taller"."expEliminarExp"(pid integer);
       Taller          postgres    false    6            �            1259    16437 
   Expediente    TABLE     �  CREATE TABLE "Taller"."Expediente" (
    "IDExpediente" integer NOT NULL,
    "IDVehiculo" integer NOT NULL,
    "nombreTecnico" character varying(250) NOT NULL,
    "Asunto" character varying(100) NOT NULL,
    "Descripcion" character varying NOT NULL,
    "fechaCreacionExp" date NOT NULL,
    "nombreCliente" character varying(250) NOT NULL,
    marca character varying(70) NOT NULL,
    modelo integer NOT NULL,
    placa character(10)
);
 "   DROP TABLE "Taller"."Expediente";
       Taller         heap    postgres    false    6                        1255    16443    expVerExpPorPlaca(character)    FUNCTION     �   CREATE FUNCTION "Taller"."expVerExpPorPlaca"(pplaca character) RETURNS SETOF "Taller"."Expediente"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Expediente"
	WHERE "Expediente"."placa" = "pplaca";
END;
$$;
 >   DROP FUNCTION "Taller"."expVerExpPorPlaca"(pplaca character);
       Taller          postgres    false    206    6            �            1255    16444    expVerExpedientes()    FUNCTION     �   CREATE FUNCTION "Taller"."expVerExpedientes"() RETURNS SETOF "Taller"."Expediente"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Expediente";
END;
$$;
 .   DROP FUNCTION "Taller"."expVerExpedientes"();
       Taller          postgres    false    6    206            �            1255    16445 A   odnActualizarOrden(integer, character varying, character varying) 	   PROCEDURE       CREATE PROCEDURE "Taller"."odnActualizarOrden"(pido integer, pced character varying, pplaca character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
	UPDATE "Taller"."Orden" SET
		"cedulaCliente" = "pced",
		"placaVehiculo" = "pplaca"
	WHERE "Orden"."IDOrden" = "pido";
END;
$$;
 n   DROP PROCEDURE "Taller"."odnActualizarOrden"(pido integer, pced character varying, pplaca character varying);
       Taller          postgres    false    6                       1255    32780 >   odnCrearOrdenFn(integer, character varying, character varying)    FUNCTION     $  CREATE FUNCTION "Taller"."odnCrearOrdenFn"(pidv integer, pdesc character varying, pcedula character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
DECLARE
-- 	descr VARCHAR;
	placaVhl CHARACTER VARYING;
BEGIN
-- 	SELECT "Servicio"."descripcion" FROM "Taller"."Servicio"
-- 	INTO descr
-- 	WHERE "Servicio"."IDVehiculo" = "pidv";
	
	SELECT "Vehiculo"."placa" FROM "Taller"."Vehiculo"
	INTO placaVhl
	WHERE "Vehiculo"."IDVehiculo" = "pidv";
	
	INSERT INTO "Taller"."Orden" VALUES
	(
		DEFAULT, "pidv", "pcedula", placaVhl, "pdesc"
	);
END;
$$;
 l   DROP FUNCTION "Taller"."odnCrearOrdenFn"(pidv integer, pdesc character varying, pcedula character varying);
       Taller          postgres    false    6            �            1255    16447    odnEliminarOrden(integer) 	   PROCEDURE     �   CREATE PROCEDURE "Taller"."odnEliminarOrden"(pid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
	DELETE FROM "Taller"."Orden" 
	WHERE "Orden"."IDOrden" = "pid";
END;
$$;
 9   DROP PROCEDURE "Taller"."odnEliminarOrden"(pid integer);
       Taller          postgres    false    6            *           1255    40973 >   odnGenerarOrden(integer, character varying, character varying)    FUNCTION     �  CREATE FUNCTION "Taller"."odnGenerarOrden"(pidcliente integer, pdescanterior character varying, pdesc character varying) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE
	k INTEGER;
	ID_VEHICULO INTEGER;
	CEDULA_CLIENTE CHARACTER(30);
	PLACA_VHL CHARACTER(10);
BEGIN
	SELECT COUNT(*) FROM "Taller"."Servicio" 
	INTO k
	WHERE "Servicio"."descripcion" LIKE "pdesc" || '%';

	IF k > 0 THEN --SI LA DESCRIPCIÓN SE CONSERVA
		SELECT "Servicio"."IDVehiculo" FROM "Taller"."Servicio"
		INTO ID_VEHICULO
		WHERE "Servicio"."descripcion" LIKE "pdesc" || '%';

		SELECT "Cliente"."cedula" FROM "Taller"."Cliente"
		INTO CEDULA_CLIENTE
		WHERE "Cliente"."IDCliente" = "pidcliente";

		SELECT "Vehiculo"."placa" FROM "Taller"."Vehiculo"
		INTO PLACA_VHL
		WHERE "Vehiculo"."IDVehiculo" = ID_VEHICULO;

		INSERT INTO "Taller"."Orden" VALUES
		(
			DEFAULT, ID_VEHICULO, CEDULA_CLIENTE, PLACA_VHL, "pdesc"
		);
		RETURN TRUE;
	END IF;

	IF K = 0 THEN --SI SE CAMBIÓ, SE HACE OTRO SERVICIO
		SELECT "Servicio"."IDVehiculo" FROM "Taller"."Servicio"
		INTO ID_VEHICULO
		WHERE "Servicio"."descripcion" LIKE "pdescanterior" || '%';
		/*
			EL ID SE OBTIENE CON LA DESCRIPCIÓN DEL SERVICIO QUE
			YA NO APLICA
		*/

		INSERT INTO "Taller"."Servicio" VALUES
		(
			DEFAULT, ID_VEHICULO, "pdesc" 
			--SE CREA UN SERVICIO CON LA DESCRIPCIÓN NUEVA
		);

		SELECT "Cliente"."cedula" FROM "Taller"."Cliente"
		INTO CEDULA_CLIENTE
		WHERE "Cliente"."IDCliente" = "pidcliente";

		SELECT "Vehiculo"."placa" FROM "Taller"."Vehiculo"
		INTO PLACA_VHL
		WHERE "Vehiculo"."IDVehiculo" = ID_VEHICULO;

		INSERT INTO "Taller"."Orden" VALUES
		(
			DEFAULT, ID_VEHICULO, CEDULA_CLIENTE, PLACA_VHL, "pdesc"
		);
		RETURN TRUE;
	ELSE
		RETURN FALSE;
	END IF;
END;
$$;
 x   DROP FUNCTION "Taller"."odnGenerarOrden"(pidcliente integer, pdescanterior character varying, pdesc character varying);
       Taller          postgres    false    6            �            1259    16448    Orden    TABLE     �   CREATE TABLE "Taller"."Orden" (
    "IDOrden" integer NOT NULL,
    "IDVehiculo" integer NOT NULL,
    "cedulaCliente" character(30) NOT NULL,
    "placaVehiculo" character(10) NOT NULL,
    "descServicio" character varying NOT NULL
);
    DROP TABLE "Taller"."Orden";
       Taller         heap    postgres    false    6            K           0    0    TABLE "Orden"    COMMENT     f   COMMENT ON TABLE "Taller"."Orden" IS 'Datos generados de una orden durante el mismo día de la cita';
          Taller          postgres    false    207            �            1255    16454    odnVerOrdenPorCedula(integer)    FUNCTION     �   CREATE FUNCTION "Taller"."odnVerOrdenPorCedula"(pid integer) RETURNS SETOF "Taller"."Orden"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Orden"
	WHERE "Orden"."IDOrden" = "pid";
END;
$$;
 <   DROP FUNCTION "Taller"."odnVerOrdenPorCedula"(pid integer);
       Taller          postgres    false    207    6            �            1255    16455    odnVerOrdenes()    FUNCTION     �   CREATE FUNCTION "Taller"."odnVerOrdenes"() RETURNS SETOF "Taller"."Orden"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Orden";
END;
$$;
 *   DROP FUNCTION "Taller"."odnVerOrdenes"();
       Taller          postgres    false    207    6                        1255    16456 )   svcActualizarServicio(integer, character) 	   PROCEDURE     �   CREATE PROCEDURE "Taller"."svcActualizarServicio"(pid integer, pdesc character)
    LANGUAGE plpgsql
    AS $$
BEGIN
	UPDATE "Taller"."Servicio" SET
		"descripcion" = "pdesc"
	WHERE "Servicio"."IDServicio" = "pid";
END;
$$;
 O   DROP PROCEDURE "Taller"."svcActualizarServicio"(pid integer, pdesc character);
       Taller          postgres    false    6            �            1255    16457 ,   svcCrearServicio(integer, character varying) 	   PROCEDURE     �   CREATE PROCEDURE "Taller"."svcCrearServicio"(pidv integer, pdesc character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
	INSERT INTO "Taller"."Servicio" VALUES
	(
		DEFAULT, "pidv", "pdesc"
	);
END;
$$;
 S   DROP PROCEDURE "Taller"."svcCrearServicio"(pidv integer, pdesc character varying);
       Taller          postgres    false    6            �            1255    16458    svcEliminarServicio(integer) 	   PROCEDURE     �   CREATE PROCEDURE "Taller"."svcEliminarServicio"(pid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
	DELETE FROM "Taller"."Servicio" 
	WHERE "Servicio"."IDServicio" = "pid";
END;
$$;
 <   DROP PROCEDURE "Taller"."svcEliminarServicio"(pid integer);
       Taller          postgres    false    6            �            1259    16459    Servicio    TABLE     �   CREATE TABLE "Taller"."Servicio" (
    "IDServicio" integer NOT NULL,
    "IDVehiculo" integer NOT NULL,
    descripcion character varying NOT NULL
);
     DROP TABLE "Taller"."Servicio";
       Taller         heap    postgres    false    6            L           0    0    TABLE "Servicio"    COMMENT     a   COMMENT ON TABLE "Taller"."Servicio" IS 'Información de un servicio. Necesario para la orden.';
          Taller          postgres    false    208            �            1255    16465    svcVerServicios()    FUNCTION     �   CREATE FUNCTION "Taller"."svcVerServicios"() RETURNS SETOF "Taller"."Servicio"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Servicio";
END;
$$;
 ,   DROP FUNCTION "Taller"."svcVerServicios"();
       Taller          postgres    false    6    208                       1255    16629 `   tncActualizarTecnico(character varying, character varying, character varying, character varying) 	   PROCEDURE     C  CREATE PROCEDURE "Taller"."tncActualizarTecnico"(pid character varying, nmb character varying, ap1 character varying, ap2 character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN
	UPDATE "Taller"."Tecnico" SET 
		"nombre" = "nmb",
		"pmrApellido" = "ap1",
		"sgndApellido" = "ap2"
	WHERE "Tecnico"."cedula" = "pid";
END
$$;
 �   DROP PROCEDURE "Taller"."tncActualizarTecnico"(pid character varying, nmb character varying, ap1 character varying, ap2 character varying);
       Taller          postgres    false    6            �            1255    16467 S   tncCrearTecnico(character varying, character varying, character varying, character) 	   PROCEDURE     	  CREATE PROCEDURE "Taller"."tncCrearTecnico"(nmb character varying, ap1 character varying, ap2 character varying, ced character)
    LANGUAGE plpgsql
    AS $$
BEGIN
	INSERT INTO "Taller"."Tecnico"
	VALUES(DEFAULT, "nmb", "ap1", "ap2", "ced", CURRENT_DATE);
END
$$;
    DROP PROCEDURE "Taller"."tncCrearTecnico"(nmb character varying, ap1 character varying, ap2 character varying, ced character);
       Taller          postgres    false    6                       1255    16631 %   tncEliminarTecnico(character varying)    FUNCTION       CREATE FUNCTION "Taller"."tncEliminarTecnico"(pid character varying) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE
	idTec INTEGER;
	k INTEGER;
	n INTEGER;
	filasAfectadas INTEGER;
BEGIN
	SELECT "Tecnico"."IDTecnico" FROM "Taller"."Tecnico"
	INTO idTec
	WHERE "Tecnico"."cedula" = "pid";
	
	SELECT COUNT(*) FROM "Taller"."Usuario"
	INTO k
	WHERE "Usuario"."IDTecnico" = idTec;
	
	SELECT COUNT(*) FROM "Taller"."DetallesTecnico"
	INTO n
	WHERE "DetallesTecnico"."IDTecnico" = idTec;
	
	IF k = 1 THEN
		RETURN FALSE;
	ELSIF n = 1 THEN
		RETURN FALSE;
	END IF;
	
	DELETE FROM "Taller"."Tecnico"
	WHERE "Tecnico"."IDTecnico" = idTec;
	GET DIAGNOSTICS filasAfectadas = ROW_COUNT;
	
	IF filasAfectadas = 0 THEN 
		RETURN FALSE;
	ELSE 
		RETURN TRUE;
	END IF;
END
$$;
 D   DROP FUNCTION "Taller"."tncEliminarTecnico"(pid character varying);
       Taller          postgres    false    6            �            1259    16469    Tecnico    TABLE       CREATE TABLE "Taller"."Tecnico" (
    "IDTecnico" integer NOT NULL,
    nombre character varying(150) NOT NULL,
    "pmrApellido" character varying(50) NOT NULL,
    "sgndApellido" character varying(50) NOT NULL,
    cedula character(30) NOT NULL,
    "fechaIngreso" date NOT NULL
);
    DROP TABLE "Taller"."Tecnico";
       Taller         heap    postgres    false    6            M           0    0    TABLE "Tecnico"    COMMENT     L   COMMENT ON TABLE "Taller"."Tecnico" IS 'Información general del técnico';
          Taller          postgres    false    209                       1255    16472 !   tncVerTecnicoPorCedula(character)    FUNCTION     �   CREATE FUNCTION "Taller"."tncVerTecnicoPorCedula"(pced character) RETURNS SETOF "Taller"."Tecnico"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Tecnico" 
	WHERE "Tecnico"."cedula" = "pced";
END;
$$;
 A   DROP FUNCTION "Taller"."tncVerTecnicoPorCedula"(pced character);
       Taller          postgres    false    6    209                       1255    16473    tncVerTecnicos()    FUNCTION     �   CREATE FUNCTION "Taller"."tncVerTecnicos"() RETURNS SETOF "Taller"."Tecnico"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Tecnico";
END;
$$;
 +   DROP FUNCTION "Taller"."tncVerTecnicos"();
       Taller          postgres    false    6    209            �            1259    16479    Usuario    TABLE     �   CREATE TABLE "Taller"."Usuario" (
    "IDTecnico" integer NOT NULL,
    correo character varying(35) NOT NULL,
    rol character(8) NOT NULL,
    "tokenCorreo" character varying,
    hash character varying NOT NULL,
    sal character varying NOT NULL
);
    DROP TABLE "Taller"."Usuario";
       Taller         heap    postgres    false    6            N           0    0    TABLE "Usuario"    COMMENT     R   COMMENT ON TABLE "Taller"."Usuario" IS 'Información de un usuario del sistema ';
          Taller          postgres    false    211                       1255    16591 #   usrBuscarUsuario(character varying)    FUNCTION     �   CREATE FUNCTION "Taller"."usrBuscarUsuario"(phash character varying) RETURNS SETOF "Taller"."Usuario"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Usuario"
	WHERE "Usuario"."hash" = "phash";
END;
$$;
 D   DROP FUNCTION "Taller"."usrBuscarUsuario"(phash character varying);
       Taller          postgres    false    6    211            %           1255    40968 `   usrCambiarContrasena(character varying, character varying, character varying, character varying)    FUNCTION     *  CREATE FUNCTION "Taller"."usrCambiarContrasena"(pcorreo character varying, phashcomp character varying, pnuevohash character varying, pnuevasal character varying) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE 
	k INTEGER;
BEGIN 
	SELECT COUNT(*) FROM "Taller"."Usuario"
	INTO k
	WHERE "Usuario"."correo" = "pcorreo"
	AND "Usuario"."hash" = "phashcomp";
	
	IF k = 0 THEN
		RETURN FALSE; 
	ELSE 
		UPDATE "Taller"."Usuario" SET
		"hash" = "pnuevohash",
		"sal" = "pnuevasal"
		WHERE "Usuario"."correo" = "pcorreo";
		RETURN TRUE;
	END IF;
END
$$;
 �   DROP FUNCTION "Taller"."usrCambiarContrasena"(pcorreo character varying, phashcomp character varying, pnuevohash character varying, pnuevasal character varying);
       Taller          postgres    false    6                       1255    16587 [   usrCrearUsuario(character varying, character varying, character varying, character varying) 	   PROCEDURE     �  CREATE PROCEDURE "Taller"."usrCrearUsuario"(pcorreo character varying, phash character varying, psal character varying, prol character varying)
    LANGUAGE plpgsql
    AS $$
DECLARE
	idTec integer;
BEGIN
	SELECT "Tecnico"."IDTecnico" 
	FROM "Taller"."Tecnico", "Taller"."DetallesTecnico"
	INTO idTec
	WHERE "Tecnico"."IDTecnico" = "DetallesTecnico"."IDTecnico" 
	AND "DetallesTecnico"."correo" = "pcorreo";
	INSERT INTO "Taller"."Usuario"
	VALUES (idTec, "pcorreo", "prol", '', "phash", "psal");
END;
$$;
 �   DROP PROCEDURE "Taller"."usrCrearUsuario"(pcorreo character varying, phash character varying, psal character varying, prol character varying);
       Taller          postgres    false    6                       1255    32778    usrEliminarUsuario(integer) 	   PROCEDURE     �   CREATE PROCEDURE "Taller"."usrEliminarUsuario"(pid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
	DELETE FROM "Taller"."Usuario" 
	WHERE "Usuario"."IDTecnico" = "pid";
END;
$$;
 ;   DROP PROCEDURE "Taller"."usrEliminarUsuario"(pid integer);
       Taller          postgres    false    6                       1255    16588     usrObtenerSal(character varying)    FUNCTION       CREATE FUNCTION "Taller"."usrObtenerSal"(pcorreo character varying) RETURNS character varying
    LANGUAGE plpgsql
    AS $$
DECLARE
	sal VARCHAR;
BEGIN
	SELECT "Usuario"."sal" 
	INTO "sal"
	FROM "Taller"."Usuario"
	WHERE "Usuario"."correo" = "pcorreo";
	RETURN "sal";
END;
$$;
 C   DROP FUNCTION "Taller"."usrObtenerSal"(pcorreo character varying);
       Taller          postgres    false    6                       1255    32794 I   usrRenovarContra(character varying, character varying, character varying)    FUNCTION     �  CREATE FUNCTION "Taller"."usrRenovarContra"(pcorreo character varying, pnuevohash character varying, pnuevasal character varying) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE
	k INTEGER;
BEGIN	
	SELECT COUNT(*) FROM "Taller"."Usuario" 
	INTO K 
	WHERE "Usuario"."correo" = "pcorreo";
	
	IF k = 0 THEN
		RETURN FALSE;
	ELSE 
		UPDATE "Taller"."Usuario" SET
		"hash" = "pnuevohash",
		"sal" = "pnuevasal" WHERE
		"Usuario"."correo" = "pcorreo";
		RETURN TRUE;
	END IF;
END;
$$;
 �   DROP FUNCTION "Taller"."usrRenovarContra"(pcorreo character varying, pnuevohash character varying, pnuevasal character varying);
       Taller          postgres    false    6                       1255    16487 #   usrValidarCorreo(character varying)    FUNCTION     B  CREATE FUNCTION "Taller"."usrValidarCorreo"(pcorreo character varying) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE 
	k INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."DetallesTecnico"
	INTO k
	WHERE "DetallesTecnico"."correo" = "pcorreo";
	
	IF K = 1 THEN
		RETURN TRUE;
	ELSE 
		RETURN FALSE;
	END IF;
END;
$$;
 F   DROP FUNCTION "Taller"."usrValidarCorreo"(pcorreo character varying);
       Taller          postgres    false    6                       1255    16488 )   usrValidarUsuarioUnico(character varying)    FUNCTION     0  CREATE FUNCTION "Taller"."usrValidarUsuarioUnico"(pcorreo character varying) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE 
	k INTEGER;
	ID_TECNICO INTEGER;
BEGIN
	SELECT "Usuario"."IDTecnico" 
	FROM "Taller"."Usuario", "Taller"."DetallesTecnico"
	INTO ID_TECNICO
	WHERE "Usuario"."IDTecnico" = "DetallesTecnico"."IDTecnico"
	AND "DetallesTecnico"."correo" = "pcorreo";

	SELECT COUNT(*) FROM "Taller"."Usuario"
	INTO k
	WHERE "Usuario"."correo" = "pcorreo";
	
	IF K > 0 OR ID_TECNICO <> 0 THEN
		RETURN FALSE;
	ELSE 
		RETURN TRUE;
	END IF;
END;
$$;
 L   DROP FUNCTION "Taller"."usrValidarUsuarioUnico"(pcorreo character varying);
       Taller          postgres    false    6                       1255    32779    usrVerUsuarios()    FUNCTION     �   CREATE FUNCTION "Taller"."usrVerUsuarios"() RETURNS SETOF "Taller"."Usuario"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Usuario";
END;
$$;
 +   DROP FUNCTION "Taller"."usrVerUsuarios"();
       Taller          postgres    false    211    6            	           1255    16489 E   vhcActualizarVehiculo(integer, character varying, integer, character) 	   PROCEDURE     6  CREATE PROCEDURE "Taller"."vhcActualizarVehiculo"(pid integer, pmarca character varying, pmodelo integer, pplaca character)
    LANGUAGE plpgsql
    AS $$
BEGIN
	UPDATE "Taller"."Vehiculo" SET 
		"marca" = "pmarca",
		"modelo" = "pmodelo",
		"placa" = "pplaca"
	WHERE "Vehiculo"."IDVehiculo" = "pid";
END;
$$;
 {   DROP PROCEDURE "Taller"."vhcActualizarVehiculo"(pid integer, pmarca character varying, pmodelo integer, pplaca character);
       Taller          postgres    false    6                       1255    16490    vhcEliminarVehiculo(integer) 	   PROCEDURE     �  CREATE PROCEDURE "Taller"."vhcEliminarVehiculo"(pid integer)
    LANGUAGE plpgsql
    AS $$
BEGIN
	--Por dependencia: Orden
	DELETE FROM "Taller"."Orden"
	WHERE "Orden"."IDVehiculo" = "pid";
	--Servicio
	DELETE FROM "Taller"."Servicio"
	WHERE "Servicio"."IDVehiculo" = "pid";
	--Expediente
	DELETE FROM "Taller"."Expediente"
	WHERE "Expediente"."IDVehiculo" = "pid";
	--Finalmente, el vehículo
	DELETE FROM "Taller"."Vehiculo"
	WHERE "Vehiculo"."IDVehiculo" = "pid";
END;
$$;
 <   DROP PROCEDURE "Taller"."vhcEliminarVehiculo"(pid integer);
       Taller          postgres    false    6                       1255    16628 M   vhcIngresarVehiculo(character varying, character varying, integer, character) 	   PROCEDURE     %  CREATE PROCEDURE "Taller"."vhcIngresarVehiculo"(pcedclt character varying, pmarca character varying, pmodelo integer, placa character)
    LANGUAGE plpgsql
    AS $$
DECLARE
	k INTEGER;
	idRegClt INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."Cliente"
	INTO k
	WHERE "Cliente"."cedula" = "pcedclt";
	
	IF k = 1 THEN 
		SELECT "Cliente"."IDCliente" FROM "Taller"."Cliente"
		INTO idRegClt
		WHERE "Cliente"."cedula" = "pcedclt";
		
		INSERT INTO "Taller"."Vehiculo" VALUES
		(
			DEFAULT, idRegClt, "pmarca", "pmodelo", "placa"
		);
	END IF;
END
$$;
 �   DROP PROCEDURE "Taller"."vhcIngresarVehiculo"(pcedclt character varying, pmarca character varying, pmodelo integer, placa character);
       Taller          postgres    false    6            �            1259    16492    Vehiculo    TABLE     �   CREATE TABLE "Taller"."Vehiculo" (
    "IDVehiculo" integer NOT NULL,
    "IDCliente" integer NOT NULL,
    marca character varying(70) NOT NULL,
    modelo integer NOT NULL,
    placa character(10) NOT NULL
);
     DROP TABLE "Taller"."Vehiculo";
       Taller         heap    postgres    false    6            O           0    0    TABLE "Vehiculo"    COMMENT     ?   COMMENT ON TABLE "Taller"."Vehiculo" IS 'Datos del vehículo';
          Taller          postgres    false    212            
           1255    16495 !   vhcVerVehiculoPorPlaca(character)    FUNCTION     �   CREATE FUNCTION "Taller"."vhcVerVehiculoPorPlaca"(pplaca character) RETURNS SETOF "Taller"."Vehiculo"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Vehiculo" 
	WHERE "Vehiculo"."placa" = "pplaca";
END;
$$;
 C   DROP FUNCTION "Taller"."vhcVerVehiculoPorPlaca"(pplaca character);
       Taller          postgres    false    212    6                       1255    16496    vhcVerVehiculos()    FUNCTION     �   CREATE FUNCTION "Taller"."vhcVerVehiculos"() RETURNS SETOF "Taller"."Vehiculo"
    LANGUAGE plpgsql
    AS $$
BEGIN
	RETURN QUERY
	SELECT * FROM "Taller"."Vehiculo";
END;
$$;
 ,   DROP FUNCTION "Taller"."vhcVerVehiculos"();
       Taller          postgres    false    6    212                       1255    32772 "   vhlValidarPlaca(character varying)    FUNCTION     /  CREATE FUNCTION "Taller"."vhlValidarPlaca"(pplaca character varying) RETURNS boolean
    LANGUAGE plpgsql
    AS $$
DECLARE
	k INTEGER;
BEGIN
	SELECT COUNT(*) FROM "Taller"."Vehiculo" 
	INTO k
	WHERE "Vehiculo"."placa" = "pplaca";
	
	IF k = 1 THEN
		RETURN FALSE;
	ELSE 
		RETURN TRUE;
	END IF;
END
$$;
 D   DROP FUNCTION "Taller"."vhlValidarPlaca"(pplaca character varying);
       Taller          postgres    false    6            �            1259    16497    Bitacora    TABLE       CREATE TABLE "Taller"."Bitacora" (
    "ID" integer NOT NULL,
    usuario character varying(70) NOT NULL,
    tabla character varying(15) NOT NULL,
    controlador character varying(30) NOT NULL,
    accion character varying(30) NOT NULL,
    fecha timestamp with time zone NOT NULL
);
     DROP TABLE "Taller"."Bitacora";
       Taller         heap    postgres    false    6            �            1259    16500    Bitacora_ID_seq    SEQUENCE     �   CREATE SEQUENCE "Taller"."Bitacora_ID_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE "Taller"."Bitacora_ID_seq";
       Taller          postgres    false    6    213            P           0    0    Bitacora_ID_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE "Taller"."Bitacora_ID_seq" OWNED BY "Taller"."Bitacora"."ID";
          Taller          postgres    false    214            �            1259    16502    Cita_IDCita_seq    SEQUENCE     �   CREATE SEQUENCE "Taller"."Cita_IDCita_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE "Taller"."Cita_IDCita_seq";
       Taller          postgres    false    6    202            Q           0    0    Cita_IDCita_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE "Taller"."Cita_IDCita_seq" OWNED BY "Taller"."Cita"."IDCita";
          Taller          postgres    false    215            �            1259    16504    Cliente_IDCliente_seq    SEQUENCE     �   CREATE SEQUENCE "Taller"."Cliente_IDCliente_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE "Taller"."Cliente_IDCliente_seq";
       Taller          postgres    false    6    201            R           0    0    Cliente_IDCliente_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE "Taller"."Cliente_IDCliente_seq" OWNED BY "Taller"."Cliente"."IDCliente";
          Taller          postgres    false    216            �            1259    16403    ClientesConCita    VIEW     0  CREATE VIEW "Taller"."ClientesConCita" AS
 SELECT "Cliente".nombre,
    "Cliente"."pmrApellido",
    "Cliente"."sgndApellido",
    "Cliente".cedula,
    "Cita".fecha,
    "Cita".hora,
    "Cita".asunto
   FROM "Taller"."Cliente",
    "Taller"."Cita"
  WHERE ("Cliente"."IDCliente" = "Cita"."IDCliente");
 &   DROP VIEW "Taller"."ClientesConCita";
       Taller          postgres    false    201    201    202    202    202    202    201    201    201    6            �            1259    16597    DetallesCliente_CodDet_seq    SEQUENCE     �   CREATE SEQUENCE "Taller"."DetallesCliente_CodDet_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 5   DROP SEQUENCE "Taller"."DetallesCliente_CodDet_seq";
       Taller          postgres    false    6    204            S           0    0    DetallesCliente_CodDet_seq    SEQUENCE OWNED BY     c   ALTER SEQUENCE "Taller"."DetallesCliente_CodDet_seq" OWNED BY "Taller"."DetallesCliente"."codDet";
          Taller          postgres    false    222            �            1259    16608    DetallesTecnico_CodDet_seq    SEQUENCE     �   CREATE SEQUENCE "Taller"."DetallesTecnico_CodDet_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 5   DROP SEQUENCE "Taller"."DetallesTecnico_CodDet_seq";
       Taller          postgres    false    6    205            T           0    0    DetallesTecnico_CodDet_seq    SEQUENCE OWNED BY     c   ALTER SEQUENCE "Taller"."DetallesTecnico_CodDet_seq" OWNED BY "Taller"."DetallesTecnico"."codDet";
          Taller          postgres    false    223            �            1259    16506    Expediente_IDExpediente_seq    SEQUENCE     �   CREATE SEQUENCE "Taller"."Expediente_IDExpediente_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 6   DROP SEQUENCE "Taller"."Expediente_IDExpediente_seq";
       Taller          postgres    false    6    206            U           0    0    Expediente_IDExpediente_seq    SEQUENCE OWNED BY     e   ALTER SEQUENCE "Taller"."Expediente_IDExpediente_seq" OWNED BY "Taller"."Expediente"."IDExpediente";
          Taller          postgres    false    217            �            1259    16508    Orden_IDOrden_seq    SEQUENCE     �   CREATE SEQUENCE "Taller"."Orden_IDOrden_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE "Taller"."Orden_IDOrden_seq";
       Taller          postgres    false    6    207            V           0    0    Orden_IDOrden_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE "Taller"."Orden_IDOrden_seq" OWNED BY "Taller"."Orden"."IDOrden";
          Taller          postgres    false    218            �            1259    16510    Servicio_IDServicio_seq    SEQUENCE     �   CREATE SEQUENCE "Taller"."Servicio_IDServicio_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 2   DROP SEQUENCE "Taller"."Servicio_IDServicio_seq";
       Taller          postgres    false    6    208            W           0    0    Servicio_IDServicio_seq    SEQUENCE OWNED BY     ]   ALTER SEQUENCE "Taller"."Servicio_IDServicio_seq" OWNED BY "Taller"."Servicio"."IDServicio";
          Taller          postgres    false    219            �            1259    16512    Tecnico_IDTecnico_seq    SEQUENCE     �   CREATE SEQUENCE "Taller"."Tecnico_IDTecnico_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE "Taller"."Tecnico_IDTecnico_seq";
       Taller          postgres    false    6    209            X           0    0    Tecnico_IDTecnico_seq    SEQUENCE OWNED BY     Y   ALTER SEQUENCE "Taller"."Tecnico_IDTecnico_seq" OWNED BY "Taller"."Tecnico"."IDTecnico";
          Taller          postgres    false    220            �            1259    16474    TecnicosConCita    VIEW     0  CREATE VIEW "Taller"."TecnicosConCita" AS
 SELECT "Tecnico".nombre,
    "Tecnico"."pmrApellido",
    "Tecnico"."sgndApellido",
    "Tecnico".cedula,
    "Cita".fecha,
    "Cita".hora,
    "Cita".asunto
   FROM "Taller"."Tecnico",
    "Taller"."Cita"
  WHERE ("Tecnico"."IDTecnico" = "Cita"."IDTecnico");
 &   DROP VIEW "Taller"."TecnicosConCita";
       Taller          postgres    false    202    209    209    209    209    209    202    202    202    6            �            1259    16514    Vehiculo_IDVehiculo_seq    SEQUENCE     �   CREATE SEQUENCE "Taller"."Vehiculo_IDVehiculo_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 2   DROP SEQUENCE "Taller"."Vehiculo_IDVehiculo_seq";
       Taller          postgres    false    212    6            Y           0    0    Vehiculo_IDVehiculo_seq    SEQUENCE OWNED BY     ]   ALTER SEQUENCE "Taller"."Vehiculo_IDVehiculo_seq" OWNED BY "Taller"."Vehiculo"."IDVehiculo";
          Taller          postgres    false    221            �           2604    16516    Bitacora ID    DEFAULT     t   ALTER TABLE ONLY "Taller"."Bitacora" ALTER COLUMN "ID" SET DEFAULT nextval('"Taller"."Bitacora_ID_seq"'::regclass);
 @   ALTER TABLE "Taller"."Bitacora" ALTER COLUMN "ID" DROP DEFAULT;
       Taller          postgres    false    214    213            �           2604    16517    Cita IDCita    DEFAULT     t   ALTER TABLE ONLY "Taller"."Cita" ALTER COLUMN "IDCita" SET DEFAULT nextval('"Taller"."Cita_IDCita_seq"'::regclass);
 @   ALTER TABLE "Taller"."Cita" ALTER COLUMN "IDCita" DROP DEFAULT;
       Taller          postgres    false    215    202            }           2604    16518    Cliente IDCliente    DEFAULT     �   ALTER TABLE ONLY "Taller"."Cliente" ALTER COLUMN "IDCliente" SET DEFAULT nextval('"Taller"."Cliente_IDCliente_seq"'::regclass);
 F   ALTER TABLE "Taller"."Cliente" ALTER COLUMN "IDCliente" DROP DEFAULT;
       Taller          postgres    false    216    201            �           2604    16599    DetallesCliente codDet    DEFAULT     �   ALTER TABLE ONLY "Taller"."DetallesCliente" ALTER COLUMN "codDet" SET DEFAULT nextval('"Taller"."DetallesCliente_CodDet_seq"'::regclass);
 K   ALTER TABLE "Taller"."DetallesCliente" ALTER COLUMN "codDet" DROP DEFAULT;
       Taller          postgres    false    222    204            �           2604    16610    DetallesTecnico codDet    DEFAULT     �   ALTER TABLE ONLY "Taller"."DetallesTecnico" ALTER COLUMN "codDet" SET DEFAULT nextval('"Taller"."DetallesTecnico_CodDet_seq"'::regclass);
 K   ALTER TABLE "Taller"."DetallesTecnico" ALTER COLUMN "codDet" DROP DEFAULT;
       Taller          postgres    false    223    205            �           2604    16519    Expediente IDExpediente    DEFAULT     �   ALTER TABLE ONLY "Taller"."Expediente" ALTER COLUMN "IDExpediente" SET DEFAULT nextval('"Taller"."Expediente_IDExpediente_seq"'::regclass);
 L   ALTER TABLE "Taller"."Expediente" ALTER COLUMN "IDExpediente" DROP DEFAULT;
       Taller          postgres    false    217    206            �           2604    16520    Orden IDOrden    DEFAULT     x   ALTER TABLE ONLY "Taller"."Orden" ALTER COLUMN "IDOrden" SET DEFAULT nextval('"Taller"."Orden_IDOrden_seq"'::regclass);
 B   ALTER TABLE "Taller"."Orden" ALTER COLUMN "IDOrden" DROP DEFAULT;
       Taller          postgres    false    218    207            �           2604    16521    Servicio IDServicio    DEFAULT     �   ALTER TABLE ONLY "Taller"."Servicio" ALTER COLUMN "IDServicio" SET DEFAULT nextval('"Taller"."Servicio_IDServicio_seq"'::regclass);
 H   ALTER TABLE "Taller"."Servicio" ALTER COLUMN "IDServicio" DROP DEFAULT;
       Taller          postgres    false    219    208            �           2604    16522    Tecnico IDTecnico    DEFAULT     �   ALTER TABLE ONLY "Taller"."Tecnico" ALTER COLUMN "IDTecnico" SET DEFAULT nextval('"Taller"."Tecnico_IDTecnico_seq"'::regclass);
 F   ALTER TABLE "Taller"."Tecnico" ALTER COLUMN "IDTecnico" DROP DEFAULT;
       Taller          postgres    false    220    209            �           2604    16523    Vehiculo IDVehiculo    DEFAULT     �   ALTER TABLE ONLY "Taller"."Vehiculo" ALTER COLUMN "IDVehiculo" SET DEFAULT nextval('"Taller"."Vehiculo_IDVehiculo_seq"'::regclass);
 H   ALTER TABLE "Taller"."Vehiculo" ALTER COLUMN "IDVehiculo" DROP DEFAULT;
       Taller          postgres    false    221    212            5          0    16497    Bitacora 
   TABLE DATA           X   COPY "Taller"."Bitacora" ("ID", usuario, tabla, controlador, accion, fecha) FROM stdin;
    Taller          postgres    false    213   �.      ,          0    16395    Cita 
   TABLE DATA           �   COPY "Taller"."Cita" ("IDCita", "IDTecnico", "cedulaCliente", fecha, hora, asunto, descripcion, "citaConfirmada", "IDCliente") FROM stdin;
    Taller          postgres    false    202   �1      +          0    16390    Cliente 
   TABLE DATA           �   COPY "Taller"."Cliente" ("IDCliente", nombre, "pmrApellido", "sgndApellido", cedula, "cltFrecuente", "fechaIngreso") FROM stdin;
    Taller          postgres    false    201   �2      -          0    16417    DetallesCliente 
   TABLE DATA           a   COPY "Taller"."DetallesCliente" ("IDCliente", direccion, telefono, correo, "codDet") FROM stdin;
    Taller          postgres    false    204   3      .          0    16427    DetallesTecnico 
   TABLE DATA           a   COPY "Taller"."DetallesTecnico" ("IDTecnico", direccion, telefono, correo, "codDet") FROM stdin;
    Taller          postgres    false    205   ^3      /          0    16437 
   Expediente 
   TABLE DATA           �   COPY "Taller"."Expediente" ("IDExpediente", "IDVehiculo", "nombreTecnico", "Asunto", "Descripcion", "fechaCreacionExp", "nombreCliente", marca, modelo, placa) FROM stdin;
    Taller          postgres    false    206   �3      0          0    16448    Orden 
   TABLE DATA           n   COPY "Taller"."Orden" ("IDOrden", "IDVehiculo", "cedulaCliente", "placaVehiculo", "descServicio") FROM stdin;
    Taller          postgres    false    207   #5      1          0    16459    Servicio 
   TABLE DATA           O   COPY "Taller"."Servicio" ("IDServicio", "IDVehiculo", descripcion) FROM stdin;
    Taller          postgres    false    208   �5      2          0    16469    Tecnico 
   TABLE DATA           q   COPY "Taller"."Tecnico" ("IDTecnico", nombre, "pmrApellido", "sgndApellido", cedula, "fechaIngreso") FROM stdin;
    Taller          postgres    false    209   ?6      3          0    16479    Usuario 
   TABLE DATA           Y   COPY "Taller"."Usuario" ("IDTecnico", correo, rol, "tokenCorreo", hash, sal) FROM stdin;
    Taller          postgres    false    211   �6      4          0    16492    Vehiculo 
   TABLE DATA           W   COPY "Taller"."Vehiculo" ("IDVehiculo", "IDCliente", marca, modelo, placa) FROM stdin;
    Taller          postgres    false    212   H9      Z           0    0    Bitacora_ID_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('"Taller"."Bitacora_ID_seq"', 45, true);
          Taller          postgres    false    214            [           0    0    Cita_IDCita_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('"Taller"."Cita_IDCita_seq"', 76, true);
          Taller          postgres    false    215            \           0    0    Cliente_IDCliente_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('"Taller"."Cliente_IDCliente_seq"', 22, true);
          Taller          postgres    false    216            ]           0    0    DetallesCliente_CodDet_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('"Taller"."DetallesCliente_CodDet_seq"', 41, true);
          Taller          postgres    false    222            ^           0    0    DetallesTecnico_CodDet_seq    SEQUENCE SET     M   SELECT pg_catalog.setval('"Taller"."DetallesTecnico_CodDet_seq"', 17, true);
          Taller          postgres    false    223            _           0    0    Expediente_IDExpediente_seq    SEQUENCE SET     N   SELECT pg_catalog.setval('"Taller"."Expediente_IDExpediente_seq"', 30, true);
          Taller          postgres    false    217            `           0    0    Orden_IDOrden_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('"Taller"."Orden_IDOrden_seq"', 30, true);
          Taller          postgres    false    218            a           0    0    Servicio_IDServicio_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('"Taller"."Servicio_IDServicio_seq"', 23, true);
          Taller          postgres    false    219            b           0    0    Tecnico_IDTecnico_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('"Taller"."Tecnico_IDTecnico_seq"', 19, true);
          Taller          postgres    false    220            c           0    0    Vehiculo_IDVehiculo_seq    SEQUENCE SET     J   SELECT pg_catalog.setval('"Taller"."Vehiculo_IDVehiculo_seq"', 18, true);
          Taller          postgres    false    221            �           2606    16525    Bitacora PK_ID 
   CONSTRAINT     T   ALTER TABLE ONLY "Taller"."Bitacora"
    ADD CONSTRAINT "PK_ID" PRIMARY KEY ("ID");
 >   ALTER TABLE ONLY "Taller"."Bitacora" DROP CONSTRAINT "PK_ID";
       Taller            postgres    false    213            �           2606    16527    Cita PK_IDCita 
   CONSTRAINT     X   ALTER TABLE ONLY "Taller"."Cita"
    ADD CONSTRAINT "PK_IDCita" PRIMARY KEY ("IDCita");
 >   ALTER TABLE ONLY "Taller"."Cita" DROP CONSTRAINT "PK_IDCita";
       Taller            postgres    false    202            �           2606    16529    Cliente PK_IDCliente 
   CONSTRAINT     a   ALTER TABLE ONLY "Taller"."Cliente"
    ADD CONSTRAINT "PK_IDCliente" PRIMARY KEY ("IDCliente");
 D   ALTER TABLE ONLY "Taller"."Cliente" DROP CONSTRAINT "PK_IDCliente";
       Taller            postgres    false    201            �           2606    16531    Expediente PK_IDExpediente 
   CONSTRAINT     j   ALTER TABLE ONLY "Taller"."Expediente"
    ADD CONSTRAINT "PK_IDExpediente" PRIMARY KEY ("IDExpediente");
 J   ALTER TABLE ONLY "Taller"."Expediente" DROP CONSTRAINT "PK_IDExpediente";
       Taller            postgres    false    206            �           2606    16533    Orden PK_IDOrden 
   CONSTRAINT     [   ALTER TABLE ONLY "Taller"."Orden"
    ADD CONSTRAINT "PK_IDOrden" PRIMARY KEY ("IDOrden");
 @   ALTER TABLE ONLY "Taller"."Orden" DROP CONSTRAINT "PK_IDOrden";
       Taller            postgres    false    207            �           2606    16535    Servicio PK_IDServicio 
   CONSTRAINT     d   ALTER TABLE ONLY "Taller"."Servicio"
    ADD CONSTRAINT "PK_IDServicio" PRIMARY KEY ("IDServicio");
 F   ALTER TABLE ONLY "Taller"."Servicio" DROP CONSTRAINT "PK_IDServicio";
       Taller            postgres    false    208            �           2606    16537    Tecnico PK_IDTecnico 
   CONSTRAINT     a   ALTER TABLE ONLY "Taller"."Tecnico"
    ADD CONSTRAINT "PK_IDTecnico" PRIMARY KEY ("IDTecnico");
 D   ALTER TABLE ONLY "Taller"."Tecnico" DROP CONSTRAINT "PK_IDTecnico";
       Taller            postgres    false    209            �           2606    16539    Vehiculo PK_IDVehiculo 
   CONSTRAINT     d   ALTER TABLE ONLY "Taller"."Vehiculo"
    ADD CONSTRAINT "PK_IDVehiculo" PRIMARY KEY ("IDVehiculo");
 F   ALTER TABLE ONLY "Taller"."Vehiculo" DROP CONSTRAINT "PK_IDVehiculo";
       Taller            postgres    false    212            �           1259    16540    fki_FK_citaClienteID    INDEX     R   CREATE INDEX "fki_FK_citaClienteID" ON "Taller"."Cita" USING btree ("IDCliente");
 ,   DROP INDEX "Taller"."fki_FK_citaClienteID";
       Taller            postgres    false    202            �           1259    16541    fki_FK_citaExpedienteID    INDEX     \   CREATE INDEX "fki_FK_citaExpedienteID" ON "Taller"."Expediente" USING btree ("IDVehiculo");
 /   DROP INDEX "Taller"."fki_FK_citaExpedienteID";
       Taller            postgres    false    206            �           1259    16542    fki_FK_citaTecnicoID    INDEX     R   CREATE INDEX "fki_FK_citaTecnicoID" ON "Taller"."Cita" USING btree ("IDTecnico");
 ,   DROP INDEX "Taller"."fki_FK_citaTecnicoID";
       Taller            postgres    false    202            �           1259    16543    fki_FK_clienteDetallesID    INDEX     a   CREATE INDEX "fki_FK_clienteDetallesID" ON "Taller"."DetallesCliente" USING btree ("IDCliente");
 0   DROP INDEX "Taller"."fki_FK_clienteDetallesID";
       Taller            postgres    false    204            �           1259    16544    fki_FK_tecnicoDetallesID    INDEX     a   CREATE INDEX "fki_FK_tecnicoDetallesID" ON "Taller"."DetallesTecnico" USING btree ("IDTecnico");
 0   DROP INDEX "Taller"."fki_FK_tecnicoDetallesID";
       Taller            postgres    false    205            �           1259    16545    fki_FK_tecnicoUsuarioID    INDEX     X   CREATE INDEX "fki_FK_tecnicoUsuarioID" ON "Taller"."Usuario" USING btree ("IDTecnico");
 /   DROP INDEX "Taller"."fki_FK_tecnicoUsuarioID";
       Taller            postgres    false    211            �           1259    16627    fki_FK_vehiculoServicioID    INDEX     \   CREATE INDEX "fki_FK_vehiculoServicioID" ON "Taller"."Servicio" USING btree ("IDVehiculo");
 1   DROP INDEX "Taller"."fki_FK_vehiculoServicioID";
       Taller            postgres    false    208            �           2606    16546    Cita FK_citaClienteID    FK CONSTRAINT     �   ALTER TABLE ONLY "Taller"."Cita"
    ADD CONSTRAINT "FK_citaClienteID" FOREIGN KEY ("IDCliente") REFERENCES "Taller"."Cliente"("IDCliente") NOT VALID;
 E   ALTER TABLE ONLY "Taller"."Cita" DROP CONSTRAINT "FK_citaClienteID";
       Taller          postgres    false    2954    202    201            �           2606    16551    Cita FK_citaTecnicoID    FK CONSTRAINT     �   ALTER TABLE ONLY "Taller"."Cita"
    ADD CONSTRAINT "FK_citaTecnicoID" FOREIGN KEY ("IDTecnico") REFERENCES "Taller"."Tecnico"("IDTecnico") NOT VALID;
 E   ALTER TABLE ONLY "Taller"."Cita" DROP CONSTRAINT "FK_citaTecnicoID";
       Taller          postgres    false    2970    209    202            �           2606    32773 $   DetallesCliente FK_clienteDetallesID    FK CONSTRAINT     �   ALTER TABLE ONLY "Taller"."DetallesCliente"
    ADD CONSTRAINT "FK_clienteDetallesID" FOREIGN KEY ("IDCliente") REFERENCES "Taller"."Cliente"("IDCliente") ON DELETE CASCADE NOT VALID;
 T   ALTER TABLE ONLY "Taller"."DetallesCliente" DROP CONSTRAINT "FK_clienteDetallesID";
       Taller          postgres    false    201    2954    204            �           2606    16561 $   DetallesTecnico FK_tecnicoDetallesID    FK CONSTRAINT     �   ALTER TABLE ONLY "Taller"."DetallesTecnico"
    ADD CONSTRAINT "FK_tecnicoDetallesID" FOREIGN KEY ("IDTecnico") REFERENCES "Taller"."Tecnico"("IDTecnico") NOT VALID;
 T   ALTER TABLE ONLY "Taller"."DetallesTecnico" DROP CONSTRAINT "FK_tecnicoDetallesID";
       Taller          postgres    false    209    205    2970            d           0    0 6   CONSTRAINT "FK_tecnicoDetallesID" ON "DetallesTecnico"    COMMENT     y   COMMENT ON CONSTRAINT "FK_tecnicoDetallesID" ON "Taller"."DetallesTecnico" IS 'Relación entre el técnico y sus datos';
          Taller          postgres    false    2979            �           2606    16566    Usuario FK_tecnicoUsuarioID    FK CONSTRAINT     �   ALTER TABLE ONLY "Taller"."Usuario"
    ADD CONSTRAINT "FK_tecnicoUsuarioID" FOREIGN KEY ("IDTecnico") REFERENCES "Taller"."Tecnico"("IDTecnico") NOT VALID;
 K   ALTER TABLE ONLY "Taller"."Usuario" DROP CONSTRAINT "FK_tecnicoUsuarioID";
       Taller          postgres    false    2970    211    209            �           2606    16571    Expediente FK_vehiculoExpID    FK CONSTRAINT     �   ALTER TABLE ONLY "Taller"."Expediente"
    ADD CONSTRAINT "FK_vehiculoExpID" FOREIGN KEY ("IDVehiculo") REFERENCES "Taller"."Vehiculo"("IDVehiculo") NOT VALID;
 K   ALTER TABLE ONLY "Taller"."Expediente" DROP CONSTRAINT "FK_vehiculoExpID";
       Taller          postgres    false    212    2973    206            �           2606    16622    Servicio FK_vehiculoServicioID    FK CONSTRAINT     �   ALTER TABLE ONLY "Taller"."Servicio"
    ADD CONSTRAINT "FK_vehiculoServicioID" FOREIGN KEY ("IDVehiculo") REFERENCES "Taller"."Vehiculo"("IDVehiculo") NOT VALID;
 N   ALTER TABLE ONLY "Taller"."Servicio" DROP CONSTRAINT "FK_vehiculoServicioID";
       Taller          postgres    false    212    2973    208            5   �  x����nAE��W�Giճ_���5+vl&��G����ۑ��R�(Y����ֽ��ݴ�����p���/�apO�q�귇��2���n�|~�ߧ���9�8
QC���>TɌ�R��]��vȚb��R�ô{%���D#bm���ڗ�#��T߅�3����@cFu	�Ja�L�L��˧O]�1�:�L�0z@E2�S��u�WR��l�
ކ5զ�bM�CE�<�D��Śz���"OZ�k5�JU�PT�B�j�Aci���ШkU�J5�C����e�i��9,��I�k��*�]�(��&��ʚ�հJ�#��Z��F�}'����h�P���qQm�����Z���ƚ���bk�l���Y���"�3Y���E�q�R��l�(��ү�I��\R]N
s���_ô_�9��������;o��լ�\����-����( ��SO��rļ&�v:����!�q2v�Sq5J��O����J��ܙJ$>�%�Ί���HYŁ$ź?<��|�@�U0i;�<+�|��ζ'^��U��o���%T|W�jy�;����Lϻ�?�!Ԫ�&�=׭���x;6eNN��j������偤��{�QK�r�o��a�K�4#W��F��9,���9��gr5{ގ��kV���,W���X-�՞��ud��v���"�=      ,   �   x���1n�0Eg���
Q�k�[�Э�0�
�2(9C��#�b�c����/����l���V�6��QXc�FԶ�k�^���GŃb�������=݂:g-Mr
p��hJ@��� ��Y�W�!D8Pr�Z5RL�{������7�d�R�0X�f�8��E�u�H~5�rx˶BJ��F�Y���$�Pԥ���Q�K�ѻ�δF���s������_%�Ȳ���T�7c�wR      +   k   x�3����,>���3,1�,�(%��=�(���DN]#S]KcsL���id`d ��52�24�t-�L��t�����I�tN,*�<�9��P� �St��b���� ��!�      -   3   x�34�L)NK,NI�4426Q�83�s�s3s���s��1������ ��      .   �   x�m���0 k{� X���Hi��1����K1�� (�5לthV}=?�?�X9<�EVN��|�����Z�͉`�ZX:(�s��*]��>7J Q[4�q����6��QG�a�m.�f�gg�}>?/�      /   %  x���1N�@E��)���vB�tI���	Tn��AY�w`ױđRP���b�AHI
:��?_���,Q�B��5ϰއ;�;[�K���N͟dC���p�^]��� MR'Y�S(��A��,Id������|g��u6|��jy?P��3���A{�\��` �$Ơkm�հ4ܔB�� ��ȴX�p�;*�H�XO�
��ܰ�! <`ݑ��5zӿ�I�%6���^(\9+ã'��+�����;���9R��D�Ɲ��[����<��ޗ�t�O�|s�O5ȋ�QE_����      0   S   x�3��44�4�5021�5�46W����ޖ�`�sbnRf�BJ�BAbqIfNNb1��V���_�el@E�J��S�����qqq BE(�      1   �   x���1�0E��>A��T`�Y��X��H���$i%�k/F`�����C��P�����h�/����gZ8������(�Y��>��v��O3X��l�C�B+�̹p%��lA'����1��+�}u��(J��~
��g�<�9����/	3%���+�>�@On      2   z   x�3�t-�L�Vp/-.I,��t�����I�tN,*�<�9��P����\����Pp�X�qq:�)��g^Y����S�Z����X�|xm"������)P��9N�u��b���� \�$�      3   o  x�=�G��@  ���
ߩZ@�[�2a@(l�BD��������'�ʯ�m�
���v�������PM�%�_�CV05�%U����V�����TB��&.�G�e�(�+�?U�%���7}�ӧSx� �q�2�+v�,���=���#�s��>�#["�U�~l�����st��_l����frj�P>W��9��{aJ�����b"��f��b	o�C��|3!k��0όg����B�4㊘nI�ԝ�Y-!ޥ��V�]
���RV<������D����o-y��fƋK1�+�3c	˕{�t/�<�wk7	`� �p���H���M3����c5y����b,����5�%��nC�#Fŏ�կʸŧM�T=��J��+��9�aQ����>�@h[w��P�k�Y-��֝^�Wk�i4㗣ُ��V�Ä��+�}���ͬ��sV�u�✉'�۝�b� !u��A��U�@`"S!V*Sp�k�sJ5�{qq�Z��\�K
��eI�tE�}EJKې ����Q�4�f�G(P#Z�j��KI��[Y[H�IK]�v�m��%�'G��� ̯O0�䒂�P
|���v �������x<�����      4   2   x�34�4��/*N�HU��4V)-J��4��4�t��47T �=... �@
     