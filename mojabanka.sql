-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Sep 10, 2024 at 08:01 PM
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
-- Database: `mojabanka`
--

-- --------------------------------------------------------

--
-- Table structure for table `klijent`
--

CREATE TABLE `klijent` (
  `id` int(11) NOT NULL,
  `ime` varchar(50) NOT NULL,
  `prezime` varchar(50) NOT NULL,
  `adresa` varchar(120) DEFAULT NULL,
  `oib` varchar(50) DEFAULT NULL,
  `email` varchar(120) DEFAULT NULL,
  `datum` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_croatian_ci;

--
-- Dumping data for table `klijent`
--

INSERT INTO `klijent` (`id`, `ime`, `prezime`, `adresa`, `oib`, `email`, `datum`) VALUES
(1, 'Marko', 'Punčec', 'Augusta Šenoe 7, Čakovec', '12312312367', 'mpuncec46@gmail.com', '2001-06-04'),
(8, 'Ivan', 'Horvat', 'Ulica kralja Zvonimira 23, 10000 Zagreb', '17380230097', 'ivan.horvat@gmail.com', '2000-03-07'),
(9, 'Ana', 'Kovačić', 'Trg bana Jelačića 5, 21000 Split', '62111758127', 'ana.kovacic@poduzece.hr', '1989-07-20'),
(10, 'Petra', 'Jurić', 'Vukovarska ulica 12, 51000 Rijeka', '54168533727', 'petar.juric@poduzece.hr', '2004-07-04'),
(11, 'Luka', 'Petrović', 'Ulica Ante Starčevića 9, 23000 Zadar', '29979202677', 'l.petrovic1@gmail.com', '2001-09-10'),
(12, 'Josip', 'Novak', 'Masarykova ulica 7, 10000 Zagreb', '29571183142', 'novak.josip91@gmail.com', '1991-01-08'),
(13, 'Marija', 'Tomić', 'Zrinsko-Frankopanska ulica 6, 21000 Split', '83053912322', 'marija.tomic11@gmail.com', '2001-12-11');

-- --------------------------------------------------------

--
-- Table structure for table `korisnik`
--

CREATE TABLE `korisnik` (
  `oib` varchar(50) NOT NULL,
  `lozinka` varchar(255) NOT NULL,
  `ovlast` varchar(5) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_croatian_ci;

--
-- Dumping data for table `korisnik`
--

INSERT INTO `korisnik` (`oib`, `lozinka`, `ovlast`) VALUES
('00000000000', 'jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=', 'AD'),
('11111111111', 'FVPMYv8kYETGg6YeID5lVBmQ5/zUr5RD0iuVV+zJrFQ=', 'ED'),
('12312312367', 'QcmR62pmJCwEVBkSRCeBg85Yz0przTcveZ5LnMAYhq8=', 'KL');

-- --------------------------------------------------------

--
-- Table structure for table `ovlast`
--

CREATE TABLE `ovlast` (
  `sifra` varchar(5) NOT NULL,
  `naziv` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_croatian_ci;

--
-- Dumping data for table `ovlast`
--

INSERT INTO `ovlast` (`sifra`, `naziv`) VALUES
('AD', 'Administrator'),
('ED', 'Editor'),
('KL', 'Klijent');

-- --------------------------------------------------------

--
-- Table structure for table `racun`
--

CREATE TABLE `racun` (
  `id` int(11) NOT NULL,
  `stanje_racun` double(12,2) DEFAULT NULL,
  `id_klijent` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_croatian_ci;

--
-- Dumping data for table `racun`
--

INSERT INTO `racun` (`id`, `stanje_racun`, `id_klijent`) VALUES
(11, 100.00, 1),
(12, 0.00, 1),
(13, 0.00, 8),
(14, 890.00, 9),
(15, 0.00, 10),
(16, 200.00, 11),
(17, 0.00, 12),
(18, 0.00, 13),
(19, 10.00, 11);

-- --------------------------------------------------------

--
-- Table structure for table `transakcija`
--

CREATE TABLE `transakcija` (
  `id` int(11) NOT NULL,
  `iznos` double(12,2) DEFAULT NULL,
  `opis` text DEFAULT NULL,
  `id_racun` int(11) DEFAULT NULL,
  `datum_vrijeme` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_croatian_ci;

--
-- Dumping data for table `transakcija`
--

INSERT INTO `transakcija` (`id`, `iznos`, `opis`, `id_racun`, `datum_vrijeme`) VALUES
(12, 100.00, 'Uplata gotovinom', 11, '2024-09-10 19:09:40'),
(13, 890.00, 'Mjesečna primanja', 14, '2024-09-10 19:11:04'),
(14, 200.00, 'Uplata', 16, '2024-09-10 19:56:22');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `klijent`
--
ALTER TABLE `klijent`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD UNIQUE KEY `constraint_name` (`oib`);

--
-- Indexes for table `korisnik`
--
ALTER TABLE `korisnik`
  ADD PRIMARY KEY (`oib`),
  ADD KEY `ovlast` (`ovlast`);

--
-- Indexes for table `ovlast`
--
ALTER TABLE `ovlast`
  ADD PRIMARY KEY (`sifra`);

--
-- Indexes for table `racun`
--
ALTER TABLE `racun`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_klijent` (`id_klijent`);

--
-- Indexes for table `transakcija`
--
ALTER TABLE `transakcija`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_racun` (`id_racun`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `klijent`
--
ALTER TABLE `klijent`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT for table `racun`
--
ALTER TABLE `racun`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT for table `transakcija`
--
ALTER TABLE `transakcija`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `korisnik`
--
ALTER TABLE `korisnik`
  ADD CONSTRAINT `korisnik_ibfk_1` FOREIGN KEY (`ovlast`) REFERENCES `ovlast` (`sifra`) ON DELETE NO ACTION;

--
-- Constraints for table `racun`
--
ALTER TABLE `racun`
  ADD CONSTRAINT `racun_ibfk_1` FOREIGN KEY (`id_klijent`) REFERENCES `klijent` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Constraints for table `transakcija`
--
ALTER TABLE `transakcija`
  ADD CONSTRAINT `transakcija_ibfk_1` FOREIGN KEY (`id_racun`) REFERENCES `racun` (`id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
