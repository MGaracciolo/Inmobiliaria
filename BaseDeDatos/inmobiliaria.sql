-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 19-09-2024 a las 21:47:31
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
  `precio` decimal(10,0) NOT NULL,
  `estado` tinyint(1) NOT NULL DEFAULT 1,
  `id_creacion` int(11) NOT NULL,
  `id_anulacion` int(11) DEFAULT NULL,
  `fecha_anulacion` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`id_contrato`, `id_inquilino`, `id_inmueble`, `desde`, `hasta`, `precio`, `estado`, `id_creacion`, `id_anulacion`, `fecha_anulacion`) VALUES
(26, 12, 9, '2024-09-16', '2024-09-26', 70000, 0, 14, NULL, NULL),
(27, 13, 10, '2024-09-01', '2024-09-05', 10000000, 1, 14, NULL, NULL),
(28, 13, 10, '2024-09-01', '2024-09-17', 10000000, 1, 25, NULL, NULL),
(29, 12, 10, '2024-09-24', '2024-10-09', 10000000, 1, 25, NULL, NULL);

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
(9, 'San Martin 4330 Torre 2 Depto 4 A', '-37.98831524574530', '-35.00', 21, 3, 5, 5, 950000.00, 1),
(10, '125 Viviendas Manzana 39 Casa 1', '-32.000', '-35,00', 23, 3, 6, 5, 100.00, 1),
(11, '125 Viviendas Manzana 39 Casa 1', '-32.000', '-35.00', 25, 3, 6, 6, 100.00, 1),
(12, 'Mod 9 Mza 13 Casa 13', '-32,000', '-35,00', 21, 3, 6, 4, 150.00, 1),
(13, '125 Viviendas Manzana 39 Casa 2', '-32.000', '-35,00', 23, 3, 6, 3, 1500000.00, 1);

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
  `concepto` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
(25, 'Ariel', 'Estrada', '35211441', 'ari@gmail.com', '2664587915', '125 Viviendas Manzana 39 Casa 2', 0),
(26, 'Nela', 'Nela', '2239999', 'nela@gmail.com', '11111', 'San Martin 4330 Piso 4 Depto A Torre 2', 1);

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
(12, 'Edificio');

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
(4, 'Comercial'),
(5, 'Recreativo');

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
(22, 'Gil', 'Gunderson', 'gil@gmail.com', 'FHwivPWQgq3r4QVb4ZEU+6SK1FkZx9c+NIG8TCrPUbA=', 2, '/avatars\\avatar_22.png'),
(23, 'Troy', 'Mcclure', 'troy@gmail.com', '3h33PKvB3zZ0+sKLDEIdAPp1izyNEkmYJ4G1+AT0XVg=', 2, '/avatars\\avatar_23.png'),
(24, 'Lionel', 'Hutz', 'lio@gmail.com', 'CWZvHOfXOzC17vIgNNiuI7e9ju6IsPqz0e9p2REDHmU=', 2, '/avatars\\avatar_24.png'),
(25, 'Gary', 'Chalmers', 'garyc@gmail.com', 'SXEs6oEo2GAFNztcu+lXu385fcwMs+h10aUdf8J7e2w=', 1, '/avatars\\avatar_25.png');

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
  MODIFY `id_contrato` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `id_inmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `id_inquilino` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `id_pago` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `id_propietario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=27;

--
-- AUTO_INCREMENT de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  MODIFY `id_tipo_inmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `uso_inmueble`
--
ALTER TABLE `uso_inmueble`
  MODIFY `id_uso_inmueble` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id_usuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=26;

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
