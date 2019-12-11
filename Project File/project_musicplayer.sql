-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 11, 2019 at 08:42 AM
-- Server version: 10.1.21-MariaDB
-- PHP Version: 7.1.1

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `project_musicplayer`
--

-- --------------------------------------------------------

--
-- Table structure for table `usertable`
--

CREATE TABLE `usertable` (
  `userid` int(4) NOT NULL,
  `username` varchar(20) NOT NULL,
  `password` varchar(100) NOT NULL,
  `salt` varchar(100) NOT NULL,
  `song` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `usertable`
--

INSERT INTO `usertable` (`userid`, `username`, `password`, `salt`, `song`) VALUES
(5, 'Tina', '234pPikTSCLPak1IcikxZRKE1pnYk9PqutSY1zZLLIo=', 'i6o2CkUgIWZB1qQ9chS+jKrKuPk=', 'E:\\Music\\01 FourFiveSeconds.mp3;E:\\Music\\04 Up (feat. Demi Lovato).mp3;'),
(6, 'Christina', '0jSxoioxDJ7gcZJXuXjAXiocW+Lmus6DQ9SBzbwWvis=', 'aFEkSyO34qtxi9AzbqHPq5oxavc=', 'E:\\Music\\01 - The Heart Wants What It Wants.mp3;E:\\Music\\01 CRUSH.mp3;E:\\Music\\01 Crush (1).mp3;E:\\Music\\01 FourFiveSeconds.mp3;E:\\Music\\01 L4L (Lookin` For Luv) (Feat. Dok2 & The Quiett).mp3;E:\\Music\\2NE1 -  (MTBD)  LIVE PERFORMANCE.mp3;E:\\Music\\2NE1 - (AON) CLAP YOUR HANDS.mp3;E:\\Music\\2NE1 - (AON) GOTTA BE YOU.mp3;E:\\Music\\2NE1 - (AON) I AM THE BEST (MOTORCYCLE VER.).mp3;'),
(8, 'Bob', 'dGhZfF0DgxR6dYTSIGbj38wavLK6meEQFhTl8omtsFE=', 'gpKjpw74tJFezwufMWm3sIG+mz4=', 'E:\\Music\\AGNEZMO - Long As I Get Paid.mp3;E:\\Music\\CL - No Better Feelin.mp3;E:\\Music\\Diplo_ Rich Chigga_ Young Thug & Rich The Kid - Bankroll.mp3;E:\\Music\\Jay Park ft. pH-1 - Love My Life.mp3;E:\\Music\\Jay Park(???) - YACHT(k) ft. Sik-K.mp3;E:\\Music\\Justin Bieber - I\'m the One.mp3;E:\\Music\\Maroon 5 - What Lovers Do ft. SZA.mp3;E:\\Music\\Rita Ora - Your Song.mp3;');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `usertable`
--
ALTER TABLE `usertable`
  ADD PRIMARY KEY (`userid`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `usertable`
--
ALTER TABLE `usertable`
  MODIFY `userid` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
