# Tugas Besar 1 IF2211 Strategi Algoritma : Pemanfaatan Algoritma Greedy dalam pembuatan bot permainan Robocode Tank Royale

# Description
Algoritma greedy adalah algoritma yang mencari solusi optimum lokal pada setiap langkah dengan harapan akan membawa hasil akhir pada optimum global. Hanya ada dua kriteria solusi optimal, yaitu solusi yang semaksimal mungkin atau solusi yang seminimal mungkin. Algoritma greedy cenderung cepat dan sederhana, tetapi algoritma ini tidak menjamin solusi yang dihasilkan adalah solusi yang paling optimal.

Bot yang kami buat memiliki strategy greedy yang berbeda, yaitu:

1. Bot 1 (AzrilKangRodok)


2. Bot 2 (AntiRodok)


3. Bot 3 (TikusKantor)
    Strategi greedy yang digunakan pada TikusKantor (bot 3) adalah memaksimalkan poin yang didapatkan dari survival time. TikusKantor akan bergerak ke titik yang paling aman dan menembakkan peluru apabila ada kesempatan menyerang. Solusi optimum lokal pada bot ini adalah titik yang paling aman. Alasan memilih algoritma ini adalah bot yang akan diuji pada waktu pengujian terbilang banyak. Oleh karena itu, dengan memilih algoritma ini bot akan bertahan hidup dengan waktu yang lebih lama.

4. Bot 4 (RusdiJoging)
   Strategi greedy yang digunakan Rusdi (bot 4) adalah memaksimalkan poin yang didapatkan dari ramming. Rusdi akan berkeliling di sekitar arena dengan harapan menghindari tembakan bot musuh. Kemudian tiap 200 turn, rusdi akan mengaktifkan mode hunting, lalu mengejar bot yang terkena radarnya.

# Requirement
- Download terlebih dahulu game engine robocode, link donwload dapat dilihat di bawah
  https://github.com/Ariel-HS/tubes1-if2211-starter-pack

- Download dotnet. Pastikan dotnet yang digunakan memiliki versi 8 ke atas. Berikut link download
  https://dotnet.microsoft.com/id-id/download



# Anggota
- Muhammad Aditya Rahmadeni / 13523028
- Aryo Bama Wiratama / 13523088
- Fiqyatul Haq Rosyidi / 13523116
