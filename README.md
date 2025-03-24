# Tugas Besar 1 IF2211 Strategi Algoritma : Pemanfaatan Algoritma Greedy dalam pembuatan bot permainan Robocode Tank Royale

# Description
Algoritma greedy adalah algoritma yang mencari solusi optimum lokal pada setiap langkah dengan harapan akan membawa hasil akhir pada optimum global. Hanya ada dua kriteria solusi optimal, yaitu solusi yang semaksimal mungkin atau solusi yang seminimal mungkin. Algoritma greedy cenderung cepat dan sederhana, tetapi algoritma ini tidak menjamin solusi yang dihasilkan adalah solusi yang paling optimal.

Bot yang kami buat memiliki strategy greedy yang berbeda, yaitu:

1. Bot 1 (AzrilKangRodok) <br>
   Strategi greedy yang digunakan pada Azril (Bot 1) adalah dengan memaksimalkan poin yang dihasilkan bot dengan menembakkan peluru sedekat mungkin dan seberat mungkin. Azril menggunakan teknik “Lock and Hunt” dimana Azril akan mengunci dan memburu bot musuh yang terbaca radar. Azril akan mengejar dan mendekati musuh dengan harapan akan memberikan akurasi (solusi optimum lokal) tembakan yang lebih tinggi. Semakin dekat dengan musuh, Azril akan menembakkan peluru yang semakin berat pula.


3. Bot 2 (AntiRodok)


4. Bot 3 (TikusKantor) <br>
    Strategi greedy yang digunakan pada TikusKantor (bot 3) adalah memaksimalkan poin yang didapatkan dari survival time. TikusKantor akan bergerak ke titik yang paling aman dan menembakkan peluru apabila ada kesempatan menyerang. Solusi optimum lokal pada bot ini adalah titik yang paling aman. Alasan memilih algoritma ini adalah bot yang akan diuji pada waktu pengujian terbilang banyak. Oleh karena itu, dengan memilih algoritma ini bot akan bertahan hidup dengan waktu yang lebih lama.

5. Bot 4 (RusdiJoging) <br>
   Strategi greedy yang digunakan Rusdi (bot 4) adalah memaksimalkan poin yang didapatkan dari ramming. Rusdi akan berkeliling di sekitar arena dengan harapan menghindari tembakan bot musuh. Kemudian tiap 200 turn, rusdi akan mengaktifkan mode hunting, lalu mengejar bot yang terkena radarnya.

# Requirement
- Download terlebih dahulu game engine robocode, link donwload dapat dilihat di bawah
  ```bash
  https://github.com/Ariel-HS/tubes1-if2211-starter-pack
  ```

- Download dotnet. Pastikan dotnet yang digunakan memiliki versi 8 ke atas. Berikut link download
  ```bash
  https://dotnet.microsoft.com/id-id/download
  ```

# Compile and Build
Berikut cara build program menggunakan windows:
1. Pindah ke folder bot yang sudah kamu buat
   ```bash
   cd yourFolder
   ```
2. Di dalam direktori, buatlah file.cmd berikut
   ```cmd
   @echo off
   REM TemplateBot.cmd - Run the bot in development or release mode
   REM Set MODE=dev for development (default, always rebuilds)
   REM Set MODE=release for release (only runs if bin exists)
   
   set MODE=dev
   
   if "%MODE%"=="dev" (
       REM Development mode: always clean, build, and run
       rmdir /s /q bin obj >nul 2>&1
       dotnet build >nul
       dotnet run --no-build >nul
   ) else if "%MODE%"=="release" (
       REM Release mode: no rebuild if bin exists
       if exist bin\ (
           dotnet run --no-build >nul
       ) else (
           dotnet build >nul
           dotnet run --no-build >nul
       )
   ) else (
       echo Error: Invalid MODE value. Use "dev" or "release".
   )
   ```

   


# Anggota
- Muhammad Aditya Rahmadeni / 13523028
- Aryo Bama Wiratama / 13523088
- Fiqyatul Haq Rosyidi / 13523116
