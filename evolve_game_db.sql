-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 01, 2025 at 06:42 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `evolve game db`
--

-- --------------------------------------------------------

--
-- Table structure for table `creatortable`
--

CREATE TABLE `creatortable` (
  `id` int(11) NOT NULL,
  `PlayerID` int(11) NOT NULL,
  `CreaturID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `creature`
--

CREATE TABLE `creature` (
  `id` int(11) NOT NULL,
  `name` varchar(10) NOT NULL,
  `Damage` float NOT NULL,
  `Health` float NOT NULL,
  `Diet` varchar(20) NOT NULL,
  `AttackSpeed` float NOT NULL,
  `MoveSpeed` float NOT NULL,
  `Population` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `creaturesgenes`
--

CREATE TABLE `creaturesgenes` (
  `id` int(11) NOT NULL,
  `Creature ID` int(11) NOT NULL,
  `Gene Id` int(11) NOT NULL,
  `on/off` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `genes`
--

CREATE TABLE `genes` (
  `id` int(11) NOT NULL,
  `name` varchar(10) NOT NULL,
  `effect` varchar(10) NOT NULL,
  `Cost` int(11) NOT NULL,
  `Recessive` tinyint(1) NOT NULL,
  `PartToEffect` varchar(10) NOT NULL,
  `GeneType` varchar(10) NOT NULL,
  `GeneFamily` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `playertable`
--

CREATE TABLE `playertable` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `DisplayName` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Table structure for table `preytable`
--

CREATE TABLE `preytable` (
  `id` int(11) NOT NULL,
  `PredatorID` int(11) NOT NULL,
  `PreyID` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `creatortable`
--
ALTER TABLE `creatortable`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `creature`
--
ALTER TABLE `creature`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `creaturesgenes`
--
ALTER TABLE `creaturesgenes`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `genes`
--
ALTER TABLE `genes`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `playertable`
--
ALTER TABLE `playertable`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `preytable`
--
ALTER TABLE `preytable`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `creatortable`
--
ALTER TABLE `creatortable`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `creature`
--
ALTER TABLE `creature`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `creaturesgenes`
--
ALTER TABLE `creaturesgenes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `genes`
--
ALTER TABLE `genes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `playertable`
--
ALTER TABLE `playertable`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `preytable`
--
ALTER TABLE `preytable`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
