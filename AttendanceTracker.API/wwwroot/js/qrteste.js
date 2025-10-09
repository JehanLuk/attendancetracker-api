const readline = require("readline").createInterface({
  input: process.stdin,
  output: process.stdout
});

readline.question("Nome: ", (nome) => {
  readline.question("Matrícula: ", (matricula) => {
    const dados = { nome, matricula };
    fs.writeFileSync("dados.json", JSON.stringify(dados, null, 2), "utf8");
    QRCode.toFile("qrcode.png", JSON.stringify(dados), () => {
      console.log("✅ QR Code e JSON criados com sucesso!");
      readline.close();
    });
  });
});

//codigo para gerar o qr code
// Instale antes os pacotes:
// npm install qrcode fs

// const QRCode = require("qrcode");
// const fs = require("fs");

// // === Dados do usuário ===
// const dados = {
//   nome: "João Silva",
//   matricula: "20251007"
// };

// // === Salvar JSON ===
// const caminhoJSON = "./dados.json";
// fs.writeFileSync(caminhoJSON, JSON.stringify(dados, null, 2), "utf8");
// console.log(`✅ Arquivo JSON salvo em: ${caminhoJSON}`);

// // === Gerar QR Code ===
// const conteudoQR = JSON.stringify(dados);
// const caminhoQR = "./qrcode.png";

// QRCode.toFile(caminhoQR, conteudoQR, {
//   color: {
//     dark: "#000000", // Cor do QR
//     light: "#ffffff" // Fundo branco
//   },
//   width: 300
// }, (err) => {
//   if (err) throw err;
//   console.log(`✅ QR Code gerado e salvo em: ${caminhoQR}`);
// });