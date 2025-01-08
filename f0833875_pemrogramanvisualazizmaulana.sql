-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Хост: 10.0.2.30
-- Время создания: Июл 18 2023 г., 06:37
-- Версия сервера: 5.7.37-40
-- Версия PHP: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `f0833875_pemrogramanvisualazizmaulana`
--

-- --------------------------------------------------------

--
-- Структура таблицы `dokter`
--

CREATE TABLE `dokter` (
  `id` int(11) NOT NULL,
  `nip` varchar(10) NOT NULL,
  `nama` varchar(20) NOT NULL,
  `jk` enum('L','P') NOT NULL DEFAULT 'L',
  `spesialis` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `dokter`
--

INSERT INTO `dokter` (`id`, `nip`, `nama`, `jk`, `spesialis`) VALUES
(1, '1001', 'dr. Fikri', 'L', 'Jantung'),
(2, '1002', 'dr. Hanan', 'L', 'Mata'),
(4, '1003', 'dr. Shinta', 'P', 'THT'),
(5, '1004', 'dr. Susi', 'P', 'dr. Kandungan');

-- --------------------------------------------------------

--
-- Структура таблицы `kamar`
--

CREATE TABLE `kamar` (
  `id_kamar` int(11) NOT NULL,
  `nama_kamar` varchar(50) COLLATE utf8_unicode_ci NOT NULL,
  `jenis_kamar` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `harga` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Дамп данных таблицы `kamar`
--

INSERT INTO `kamar` (`id_kamar`, `nama_kamar`, `jenis_kamar`, `harga`) VALUES
(1, 'Kamar 101', 'Single', 100000),
(2, 'Kamar 202', 'Double', 150000);

-- --------------------------------------------------------

--
-- Структура таблицы `mahasiswa`
--

CREATE TABLE `mahasiswa` (
  `id` int(11) NOT NULL,
  `nim` varchar(10) NOT NULL,
  `nama` varchar(50) NOT NULL,
  `jk` enum('L','P') NOT NULL DEFAULT 'L',
  `prodi` enum('IND','TIF','PET') NOT NULL DEFAULT 'TIF'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `mahasiswa`
--

INSERT INTO `mahasiswa` (`id`, `nim`, `nama`, `jk`, `prodi`) VALUES
(2, '1002', 'Rita Butar-butar', 'P', 'TIF'),
(4, '1003', 'Risna Yulianti', 'P', 'IND'),
(9, '1007', 'Risna', 'P', 'TIF'),
(10, '1008', 'Susan Cute', 'P', 'IND'),
(12, '5567', 'Rahma', 'P', 'IND'),
(13, '6678', 'Haris', 'P', 'IND'),
(15, '1011', 'Susanto Budi', 'L', 'IND'),
(22, '', '', '', '');

-- --------------------------------------------------------

--
-- Структура таблицы `matakuliah`
--

CREATE TABLE `matakuliah` (
  `idmk` int(11) NOT NULL,
  `kodemk` varchar(10) NOT NULL,
  `namamk` varchar(100) NOT NULL,
  `sks` int(11) NOT NULL,
  `prodi` char(5) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `matakuliah`
--

INSERT INTO `matakuliah` (`idmk`, `kodemk`, `namamk`, `sks`, `prodi`) VALUES
(1, '2010', 'Pemrograman Visual', 3, 'TIF'),
(6, '', '', 0, '');

-- --------------------------------------------------------

--
-- Структура таблицы `pasien`
--

CREATE TABLE `pasien` (
  `id` int(11) NOT NULL,
  `nrp` int(11) NOT NULL,
  `nama` varchar(30) NOT NULL,
  `jk` enum('L','P') NOT NULL DEFAULT 'L',
  `tgl_lahir` date NOT NULL,
  `alamat` varchar(50) NOT NULL,
  `kota` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `pasien`
--

INSERT INTO `pasien` (`id`, `nrp`, `nama`, `jk`, `tgl_lahir`, `alamat`, `kota`) VALUES
(1, 1001, 'Gunawan', 'L', '1970-07-03', 'Jl. Pekalipan', 'Cirebon'),
(2, 1002, 'Sanusi', 'L', '1961-07-11', 'Jl. Kesambi', 'Cirebon'),
(3, 1003, 'Fatimah', 'P', '1988-07-03', 'Jl. Pasuketan', 'Cirebon'),
(4, 1004, 'Sumantri', 'L', '1990-07-19', 'Jl. Kanggraksan', 'Cirebon'),
(5, 1111, 'zet', 'L', '1988-12-25', 'Jl. Depok', 'Cirebon'),
(14, 0, 'saya', 'L', '2000-03-08', 'Palimanan', 'Cirebon');

-- --------------------------------------------------------

--
-- Структура таблицы `penyewa`
--

CREATE TABLE `penyewa` (
  `id_penyewa` int(11) NOT NULL,
  `nama_penyewa` varchar(100) COLLATE utf8_unicode_ci NOT NULL,
  `telepon` varchar(20) COLLATE utf8_unicode_ci NOT NULL,
  `id_kamar` int(11) DEFAULT NULL,
  `harga` int(10) NOT NULL,
  `tanggal_checkin` date NOT NULL,
  `tanggal_checkout` date NOT NULL,
  `jumlah_malam` int(11) NOT NULL,
  `total_biaya` int(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;

--
-- Дамп данных таблицы `penyewa`
--

INSERT INTO `penyewa` (`id_penyewa`, `nama_penyewa`, `telepon`, `id_kamar`, `harga`, `tanggal_checkin`, `tanggal_checkout`, `jumlah_malam`, `total_biaya`) VALUES
(1, 'Aziz', '083824082456', 1, 100000, '2023-07-17', '2023-07-19', 2, 200000),
(2, 'Maulana', '08123456789', 2, 150000, '2023-07-17', '2023-07-18', 1, 150000);

-- --------------------------------------------------------

--
-- Структура таблицы `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `passwd` varchar(50) NOT NULL,
  `rolename` enum('admin','penyewa') NOT NULL DEFAULT 'penyewa'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Дамп данных таблицы `users`
--

INSERT INTO `users` (`id`, `username`, `passwd`, `rolename`) VALUES
(9, 'Aziz', '202cb962ac59075b964b07152d234b70', 'admin'),
(10, 'Maulana', '250cf8b51c773f3f8dc8b4be867a9a02', 'penyewa');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `dokter`
--
ALTER TABLE `dokter`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `unik` (`nip`);

--
-- Индексы таблицы `kamar`
--
ALTER TABLE `kamar`
  ADD PRIMARY KEY (`id_kamar`);

--
-- Индексы таблицы `mahasiswa`
--
ALTER TABLE `mahasiswa`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `nim` (`nim`);

--
-- Индексы таблицы `matakuliah`
--
ALTER TABLE `matakuliah`
  ADD PRIMARY KEY (`idmk`),
  ADD UNIQUE KEY `unik` (`kodemk`);

--
-- Индексы таблицы `pasien`
--
ALTER TABLE `pasien`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `nrp` (`nrp`);

--
-- Индексы таблицы `penyewa`
--
ALTER TABLE `penyewa`
  ADD PRIMARY KEY (`id_penyewa`),
  ADD KEY `id_kamar` (`id_kamar`);

--
-- Индексы таблицы `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `idx` (`username`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `dokter`
--
ALTER TABLE `dokter`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT для таблицы `kamar`
--
ALTER TABLE `kamar`
  MODIFY `id_kamar` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT для таблицы `mahasiswa`
--
ALTER TABLE `mahasiswa`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=23;

--
-- AUTO_INCREMENT для таблицы `matakuliah`
--
ALTER TABLE `matakuliah`
  MODIFY `idmk` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT для таблицы `pasien`
--
ALTER TABLE `pasien`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT для таблицы `penyewa`
--
ALTER TABLE `penyewa`
  MODIFY `id_penyewa` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT для таблицы `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `penyewa`
--
ALTER TABLE `penyewa`
  ADD CONSTRAINT `penyewa_ibfk_1` FOREIGN KEY (`id_kamar`) REFERENCES `kamar` (`id_kamar`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
