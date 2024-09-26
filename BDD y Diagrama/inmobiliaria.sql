-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 26-09-2024 a las 23:15:02
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria`
--
CREATE DATABASE IF NOT EXISTS `inmobiliaria` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci;
USE `inmobiliaria`;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `id_contrato` int(11) NOT NULL,
  `id_inquilino` int(11) NOT NULL,
  `id_inmueble` int(11) NOT NULL,
  `desde` date NOT NULL,
  `hasta` date NOT NULL,
  `meses` int(11) NOT NULL DEFAULT 0,
  `precio` decimal(10,2) NOT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT 1,
  `id_creacion` int(11) NOT NULL,
  `id_anulacion` int(11) DEFAULT NULL,
  `fecha_anulacion` date DEFAULT NULL,
  `multa` decimal(10,2) NOT NULL DEFAULT 0.00
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`id_contrato`, `id_inquilino`, `id_inmueble`, `desde`, `hasta`, `meses`, `precio`, `estado`, `id_creacion`, `id_anulacion`, `fecha_anulacion`, `multa`) VALUES
(30, 12, 10, '2024-09-21', '2025-09-21', 13, 250000.00, 1, 25, 25, '2024-09-22', 0.00),
(31, 13, 11, '2024-09-24', '2024-09-30', 1, 250000.00, 1, 25, NULL, NULL, 0.00),
(32, 14, 10, '2024-11-10', '2025-01-30', 3, 750000.00, 0, 25, 25, '2024-09-23', 0.00),
(33, 12, 10, '2025-01-07', '2027-10-06', 34, 6000.00, 1, 25, 25, '2024-09-23', 0.00),
(34, 15, 12, '2016-01-01', '2028-12-31', 159, 10.00, 1, 25, NULL, NULL, 0.00),
(35, 16, 14, '2012-01-23', '2027-10-19', 192, 10.00, 0, 25, 25, '2024-09-25', 20.00),
(39, 16, 14, '2027-10-20', '2027-10-29', 1, 10.00, 1, 25, NULL, NULL, 0.00),
(40, 14, 10, '2029-01-01', '2029-01-03', 1, 250000.00, 0, 25, 25, '2024-09-25', 500000.00);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `id_inmueble` int(11) NOT NULL,
  `direccion` varchar(150) NOT NULL,
  `latitud` varchar(50) NOT NULL,
  `longitud` varchar(50) NOT NULL,
  `id_propietario` int(11) NOT NULL,
  `id_uso_inmueble` int(11) NOT NULL,
  `id_tipo_inmueble` int(11) NOT NULL,
  `ambientes` int(11) NOT NULL,
  `precio` decimal(10,2) NOT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`id_inmueble`, `direccion`, `latitud`, `longitud`, `id_propietario`, `id_uso_inmueble`, `id_tipo_inmueble`, `ambientes`, `precio`, `estado`) VALUES
(9, 'San Martin 4330 Torre 2 Depto 4 A', '-37.98831524574530', '-35.00', 22, 3, 5, 5, 950000.00, 0),
(10, '125 Viviendas Manzana 39 Casa 2', '-32.000', '-35,00', 23, 3, 6, 5, 750000.00, 1),
(11, '125 Viviendas Manzana 39 Casa 1', '-32.000', '-35.00', 25, 3, 6, 6, 100.00, 1),
(12, 'Mod 9 Mza 13 Casa 13', '-32,000', '-35,00', 21, 3, 6, 4, 150.00, 1),
(14, 'San Juan 720', '-39.2000', '-38.2000', 22, 4, 15, 2, 950000.00, 0),
(15, 'Gutemberg 123', '-96.213', '-98.321', 24, 3, 11, 8, 1500000.00, 1),
(16, 'Av. Tobar Garcia', '-37.98831524574530', '-38.2000', 25, 4, 14, 1, 250000.00, 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `id_inquilino` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `dni` varchar(50) NOT NULL,
  `email` varchar(50) NOT NULL,
  `telefono` varchar(50) NOT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`id_inquilino`, `nombre`, `apellido`, `dni`, `email`, `telefono`, `estado`) VALUES
(12, 'Ambar', 'Vera', '98654321', 'ambar@gmail.com', '011447799', 1),
(13, 'Runa', 'Vera', '97987654', 'runa@gmail.com', '223011001', 1),
(14, 'Marianela', 'Garacciolo', '40730555', 'neli@gmail.com', '22336699496', 1),
(15, 'Cateto', 'Aloi', '96541632', 'cateto@gmail.com', '22336699496', 1),
(16, 'Enzo', 'Garacciolo', '45211777', 'enzo@gmail.com', '22336699496', 1),
(17, 'Nelo', 'Posse', '39633522', 'nelido@gmail.com', '223998877', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `id_pago` int(11) NOT NULL,
  `id_contrato` int(11) NOT NULL,
  `numero` int(11) NOT NULL,
  `importe` decimal(10,2) NOT NULL,
  `fecha` date NOT NULL,
  `id_creacion` int(11) NOT NULL,
  `id_anulacion` int(11) DEFAULT NULL,
  `concepto` varchar(50) DEFAULT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`id_pago`, `id_contrato`, `numero`, `importe`, `fecha`, `id_creacion`, `id_anulacion`, `concepto`, `estado`) VALUES
(22, 35, 1, 70.00, '2024-09-24', 25, NULL, 'Septiembre', 1),
(23, 35, 2, 70.00, '2024-09-24', 25, NULL, 'Septiembre', 1),
(24, 39, 1, 10.00, '2024-09-25', 22, NULL, 'Septiembre', 1),
(25, 39, 2, 10.00, '2024-09-25', 22, NULL, 'Septiembre', 1),
(26, 33, 1, 75000.00, '2024-09-26', 25, NULL, 'Enero 1/3', 1),
(27, 33, 2, 75000.00, '2024-09-26', 25, NULL, 'Enero 1/3', 1);

--
-- Disparadores `pago`
--
DELIMITER $$
CREATE TRIGGER `before_pago_insert` BEFORE INSERT ON `pago` FOR EACH ROW BEGIN
    DECLARE ultimoNumero INT;

    -- Obtener el último número de pago para el contrato
    SELECT IFNULL(MAX(numero), 0) INTO ultimoNumero
    FROM pago
    WHERE id_contrato = NEW.id_contrato;

    -- Asignar el siguiente número de pago
    SET NEW.numero = ultimoNumero + 1;
END
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `id_propietario` int(11) NOT NULL,
  `nombre` varchar(50) NOT NULL,
  `apellido` varchar(50) NOT NULL,
  `dni` varchar(50) NOT NULL,
  `email` varchar(50) DEFAULT NULL,
  `telefono` varchar(50) DEFAULT NULL,
  `direccion` varchar(150) NOT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`id_propietario`, `nombre`, `apellido`, `dni`, `email`, `telefono`, `direccion`, `estado`) VALUES
(21, 'Carina', 'Aloi', '20619618', 'cari@gmail.com', '2664587912', 'Modulo 9 Manzana 13 Casa 13', 1),
(22, 'Maria', 'Rinaldi', '08900600', 'mari@gmail.com', '011535353', 'San Martin 4330 Piso 4 Depto A Torre 2', 1),
(23, 'Roberto', 'Vera', '26500400', 'roberto@gmail.com', '2664151515', '125 Viviendas Manzana 39 Casa 1', 1),
(24, 'Carlos ', 'Posse', '30369257', 'carli@gmail.com', '223998877', 'Gutemberg 956', 1),
(25, 'Ariel', 'Estrada', '35211441', 'ari@gmail.com', '2664587915', '125 Viviendas Manzana 39 Casa 2', 1),
(26, 'Nela', 'Garacciolo', '45123782', 'nela@gmail.com', '0112236646', 'Buenos Aires 1998 Depto 1', 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_inmueble`
--

CREATE TABLE `tipo_inmueble` (
  `id_tipo_inmueble` int(11) NOT NULL,
  `valor` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipo_inmueble`
--

INSERT INTO `tipo_inmueble` (`id_tipo_inmueble`, `valor`) VALUES
(5, 'Departamento'),
(6, 'Casa'),
(7, 'Terreno'),
(9, 'Oficina'),
(11, 'Duplex'),
(12, 'Edificio'),
(14, 'Depósito'),
(15, 'Local');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `uso_inmueble`
--

CREATE TABLE `uso_inmueble` (
  `id_uso_inmueble` int(11) NOT NULL,
  `valor` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `uso_inmueble`
--

INSERT INTO `uso_inmueble` (`id_uso_inmueble`, `valor`) VALUES
(3, 'Residencial'),
(4, 'Comercial');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `id_usuario` int(11) NOT NULL,
  `nombre` varchar(60) NOT NULL,
  `apellido` varchar(60) NOT NULL,
  `email` varchar(60) NOT NULL,
  `password` varchar(100) DEFAULT NULL,
  `rol` int(11) NOT NULL,
  `avatar` varchar(150) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`id_usuario`, `nombre`, `apellido`, `email`, `password`, `rol`, `avatar`) VALUES
(22, 'Gil', 'Gundersons', 'gil@gmail.com', 'xU2VUIY7xo7RHRM5hIcmpQp61v9OmBwRPzsNefIKI/U=', 2, '/avatars\\avatar_22.png'),
(23, 'Troy', 'Mcclure', 'troy@gmail.com', '3h33PKvB3zZ0+sKLDEIdAPp1izyNEkmYJ4G1+AT0XVg=', 2, '/avatars\\avatar_23.png'),
(24, 'Lionel', 'Hutz', 'lio@gmail.com', 'CWZvHOfXOzC17vIgNNiuI7e9ju6IsPqz0e9p2REDHmU=', 2, '/avatars\\avatar_24.png'),
(25, 'Gary', 'Chalmers', 'gary@gmail.com', 'SXEs6oEo2GAFNztcu+lXu385fcwMs+h10aUdf8J7e2w=', 1, '/avatars\\avatar_25.png'),
(26, 'Patricio', 'Estrella', 'patella@gmail.com', '6DtV2R/ncIM0fY37FnUqYXJzM/KUuisOeUc53HR8xAE=', 2, NULL),
(27, 'Otto', 'Mann', 'otto@gmail.com', 'h0ZhHoNXOejrXiwUCQy9p3JHPiHZEbQpnDmNFQKKwQU=', 1, '/avatars\\avatar_27.png'),
(29, 'Gary', 'Caracol', 'caracol@gmail.com', 'l9ReIfbH4VtjykfC52MibyPTJF1w+4RUrObI66RLIlY=', 2, '/avatars\\avatar_29.png');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`id_contrato`),
  ADD KEY `id_inquilino` (`id_inquilino`,`id_inmueble`),
  ADD KEY `contrato_ibfk_2` (`id_inmueble`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`id_inmueble`),
  ADD KEY `id_uso_inmueble` (`id_uso_inmueble`,`id_tipo_inmueble`),
  ADD KEY `id_tipo_inmueble` (`id_tipo_inmueble`),
  ADD KEY `inmueble_ibfk_2` (`id_propietario`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`id_inquilino`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`id_pago`),
  ADD KEY `id_contrato` (`id_contrato`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`id_propietario`);

--
-- Indices de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  ADD PRIMARY KEY (`id_tipo_inmueble`);

--
-- Indices de la tabla `uso_inmueble`
--
ALTER TABLE `uso_inmueble`
  ADD PRIMARY KEY (`id_uso_inmueble`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id_usuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `id_contrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=41;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `id_inmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `id_inquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `id_pago` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `id_propietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  MODIFY `id_tipo_inmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT de la tabla `uso_inmueble`
--
ALTER TABLE `uso_inmueble`
  MODIFY `id_uso_inmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id_usuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`id_inquilino`) REFERENCES `inquilino` (`id_inquilino`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`id_inmueble`) REFERENCES `inmueble` (`id_inmueble`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_2` FOREIGN KEY (`id_propietario`) REFERENCES `propietario` (`id_propietario`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `inmueble_ibfk_3` FOREIGN KEY (`id_tipo_inmueble`) REFERENCES `tipo_inmueble` (`id_tipo_inmueble`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `inmueble_ibfk_4` FOREIGN KEY (`id_uso_inmueble`) REFERENCES `uso_inmueble` (`id_uso_inmueble`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`id_contrato`) REFERENCES `contrato` (`id_contrato`) ON DELETE NO ACTION ON UPDATE NO ACTION;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
