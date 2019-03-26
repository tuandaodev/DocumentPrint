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
-- Table structure for table `mahinhanh`
--

CREATE TABLE `mahinhanh` (
  `id` int(11) NOT NULL,
  `MaSP` varchar(100) DEFAULT NULL,
  `MaHinhAnh` varchar(100) DEFAULT NULL,
  `AnhSP` varchar(100) DEFAULT NULL,
  `MaNV` int(11) DEFAULT NULL,
  `ThoiGian` datetime DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `mahinhanh`
--

INSERT INTO `mahinhanh` (`id`, `MaSP`, `MaHinhAnh`, `AnhSP`, `MaNV`, `ThoiGian`) VALUES
(1, 'XM15S', 'MHA3', '658874511dang-ky-nhan-hieu-cho-quan-ao.jpg', 11125, '2019-03-23 16:08:10'),
(3, 'XM13M', 'MHA2', '737823486Hinh-anh-dep-girl-xinh-de-thuong-nhat-nam-mau-tuat-2018.jpg', 11125, '2019-03-23 16:52:06'),
(4, 'XM14XXL', 'MHA1', '210876464bo-quan-ao-momma-baby-be-trai-dai-tay-116157_1.jpg', 11125, '2019-03-25 02:30:16'),
(5, 'AM1001L', 'MHA4', '2321777341.jpg', 11125, '2019-03-27 00:42:27'),
(6, 'SS203S', 'MHA5', '8091125482.png', 11125, '2019-03-27 00:42:54'),
(7, 'BC292S', 'MHA6', '9291076664.jpg', 11125, '2019-03-27 00:43:08'),
(8, 'AM1001S', 'MHA7', '4383239743.jpg', 11125, '2019-03-27 00:43:38'),
(9, 'MT21L', 'MHA8', '713500975.png', 11125, '2019-03-27 00:43:53');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `mahinhanh`
--
ALTER TABLE `mahinhanh`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `mahinhanh`
--
ALTER TABLE `mahinhanh`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
