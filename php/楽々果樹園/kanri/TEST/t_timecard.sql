-- phpMyAdmin SQL Dump
-- version 5.2.1-1.el8.remi
-- https://www.phpmyadmin.net/
--
-- ホスト: localhost
-- 生成日時: 2025 年 7 月 18 日 10:37
-- サーバのバージョン： 10.5.22-MariaDB-log
-- PHP のバージョン: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- データベース: `ss611756_db`
--

-- --------------------------------------------------------

--
-- テーブルの構造 `t_timecard`
--

CREATE TABLE `t_timecard` (
  `id` INT AUTO_INCREMENT PRIMARY KEY,
  `adres` varchar(20) NOT NULL,
  `tm` datetime NOT NULL,
  `gps` varchar(40) NOT NULL,
  `work` int(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- テーブルのデータのダンプ `t_timecard`
--

INSERT INTO `t_timecard` (`adres`, `tm`, `gps`, `work`) VALUES
('1234567890', '2025-07-17 17:01:18', '[緯度経度test]', 1),
('1234567890', '2025-07-18 09:38:04', '[緯度経度test]', 2),
('1234567890abc', '2025-07-15 09:00:00', '34.862751', 1),
('1234567890abc', '2025-07-15 16:32:21', '34.862540', 2),
('1234567890abc', '2025-07-16 16:10:25', '34.862751', 2),
('1234567890abc', '2025-07-16 16:10:51', '34.862751', 2),
('1234567890abc', '2025-07-16 16:11:09', '34.862751', 2),
('1234567890abc', '2025-07-16 16:11:31', '34.862751', 2),
('a', '2025-07-16 18:15:16', '[緯度経度test]', 1),
('abc', '2025-07-16 17:19:45', '[緯度経度test]', 1),
('abc', '2025-07-16 17:24:37', '[緯度経度test]', 1),
('abc', '2025-07-16 17:53:42', '[緯度経度test]', 1),
('abc', '2025-07-16 18:15:06', '[緯度経度test]', 1);

--
-- ダンプしたテーブルのインデックス
--

--
-- テーブルのインデックス `t_timecard`
--
ALTER TABLE `t_timecard`
  ADD UNIQUE KEY `t_timecard_pk` (`adres`,`tm`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
