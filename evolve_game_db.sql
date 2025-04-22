-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Apr 22, 2025 at 09:35 AM
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

--
-- Dumping data for table `creatortable`
--

INSERT INTO `creatortable` (`id`, `PlayerID`, `CreaturID`) VALUES
(1, 2, 1),
(2, 2, 2),
(3, 2, 3),
(5, 1, 4),
(14, 14, 15),
(15, 15, 16),
(16, 17, 17),
(17, 19, 1);

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

--
-- Dumping data for table `creature`
--

INSERT INTO `creature` (`id`, `name`, `Damage`, `Health`, `Diet`, `AttackSpeed`, `MoveSpeed`, `Population`) VALUES
(1, 'bunny', 1, 5, 'plants', 1, 10, 100),
(2, 'Wolf', 2, 5, 'Meat', 2, 5, 10),
(3, 'Werewolf', 5, 10, 'Meat', 2, 2, 5),
(4, 'worm', 1, 1, 'all', 1, 1, 1000),
(11, 'bear', 1, 5, 'all', 1, 1, 5),
(12, 'godzilla', 1, 5, 'all', 1, 1, 5),
(15, 'zombie', 1, 5, 'all', 1, 1, 5),
(16, 'dog', 1, 5, 'plants', 1, 1, 5),
(17, 'kujo', 1, 5, 'Meat', 1, 1, 5),
(18, 'zebra', 1, 5, 'plants', 1, 1, 5);

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
  `effect` varchar(100) NOT NULL,
  `Cost` int(11) NOT NULL,
  `Recessive` tinyint(1) NOT NULL,
  `PartToEffect` varchar(10) NOT NULL,
  `GeneType` varchar(10) NOT NULL,
  `GeneFamily` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `genes`
--

INSERT INTO `genes` (`id`, `name`, `effect`, `Cost`, `Recessive`, `PartToEffect`, `GeneType`, `GeneFamily`) VALUES
(1, 'claws', 'damage 5', 3, 0, 'arms', 'transform', 'werewolf'),
(2, 'crab arms', 'damage 5', 3, 0, 'arms', 'transform', 'crab'),
(3, 'hard shell', 'health 5', 3, 0, 'body', 'transform', 'crab'),
(4, 'stronger j', 'attackspeed 5', 10, 0, 'body', 'enhance', 'rabbit'),
(5, 'faster leg', 'movespeed 5', 5, 0, 'body', 'transform', 'rabbit');

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

--
-- Dumping data for table `playertable`
--

INSERT INTO `playertable` (`id`, `username`, `password`, `DisplayName`) VALUES
(1, 'testuser', '123456', 'test'),
(2, 'dexterrkk', 'YUio1578', 'Creator'),
(14, 'dexterrkk2', '12', 'the goat'),
(15, 'dexterrkk3', '12', 'somebody'),
(16, 'dexterrkk4', '34', 'Yonkus'),
(17, 'dexterrkk6', '34', 'goku'),
(18, 'dexterrkk5', '345', 'keemstar'),
(19, 'dexterrkk7', '67', 'moon');

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
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `creature`
--
ALTER TABLE `creature`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `creaturesgenes`
--
ALTER TABLE `creaturesgenes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `genes`
--
ALTER TABLE `genes`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `playertable`
--
ALTER TABLE `playertable`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `preytable`
--
ALTER TABLE `preytable`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
