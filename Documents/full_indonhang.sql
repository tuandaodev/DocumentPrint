-- phpMyAdmin SQL Dump
-- version 4.8.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Mar 26, 2019 at 07:50 PM
-- Server version: 10.1.34-MariaDB
-- PHP Version: 5.6.37

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `khos2`
--

-- --------------------------------------------------------

--
-- Table structure for table `indonhang`
--

CREATE TABLE `indonhang` (
  `id` int(11) NOT NULL,
  `MaDH` varchar(45) DEFAULT NULL,
  `MaHinhAnh` varchar(45) DEFAULT NULL,
  `TrangThai` int(11) DEFAULT '0',
  `ThoiGianCapNhat` datetime DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `indonhang`
--

INSERT INTO `indonhang` (`id`, `MaDH`, `MaHinhAnh`, `TrangThai`, `ThoiGianCapNhat`) VALUES
(1, 'XH0203TH1020', 'MHA1', 1, '2019-03-27 00:29:51'),
(2, 'XH0203TH1020', 'MHA2', 1, '2019-03-27 00:29:56'),
(3, 'XH0203TH1020', 'MHA3', 1, '2019-03-27 00:29:59'),
(4, 'NT0203NN1018', 'MHA4', 1, '2019-03-27 00:30:58'),
(5, 'NT0203NN1018', 'MHA5', 1, '2019-03-27 00:31:00'),
(6, 'NT0203NN1018', 'MHA6', 1, '2019-03-27 00:31:04'),
(7, 'NT0203NN1018', 'MHA7', 1, '2019-03-27 00:31:22'),
(8, 'NT0203NN1018', 'MHA8', 1, '2019-03-27 00:31:25');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `indonhang`
--
ALTER TABLE `indonhang`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `indonhang`
--
ALTER TABLE `indonhang`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
