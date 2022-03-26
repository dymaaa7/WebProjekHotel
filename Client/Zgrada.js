import { Prijava } from "./Prijava.js";

export class Zgrada{
    constructor(imeZgrade,grad) {
        this.imeZgrade = imeZgrade;
        this.grad = grad;
        this.kontejner = null;
    }

    crtaj(kontejner) {
        this.kontejner = kontejner;        

        let divZaPrijavu = document.createElement("div");
        divZaPrijavu.className = "prijavaDiv";
        this.kontejner.appendChild(divZaPrijavu);  

        let divK = document.createElement("div");
        divK.className = "kDiv";
        divZaPrijavu.appendChild(divK);

        let naslov = document.createElement("h2");
        naslov.innerHTML = this.imeZgrade;
        divK.appendChild(naslov)

        
        let lbl=document.createElement("label");
        lbl.innerHTML="Broj licence zaposlenog:";
        divK.appendChild(lbl);
        let licencaSelect = document.createElement("select");
        licencaSelect.class="licencaSelect";
        divK.appendChild(licencaSelect);
        this.ucitajLicence(licencaSelect);

        lbl=document.createElement("label");
        lbl.innerHTML="Broj pasosa gosta:";
        divK.appendChild(lbl);
        let pasosSelect = document.createElement("select");
        pasosSelect.class = "pasosSelect";
        divK.appendChild(pasosSelect);
        this.ucitajPasose(pasosSelect);


        lbl=document.createElement("label");
        lbl.innerHTML="Broj sobe:";
        divK.appendChild(lbl);
        let brojSobe=document.createElement("input");
        brojSobe.type="number";
        divK.appendChild(brojSobe);
        

        //URAADI OVO////////////////////////////////////////////////////////////////////////
        let dugmePrijavi = document.createElement("button");
        dugmePrijavi.innerHTML = "Prijavi";
        dugmePrijavi.className="nazad";
        dugmePrijavi.onclick = () => {
            fetch("https://localhost:5001/Prijava/DodajPrijavu/" + this.imeZgrade+"/"+licencaSelect.value+"/"+pasosSelect.value+"/"+brojSobe.value, {
                method: "POST"
            }).then((s) => {
                if (s.ok) {
                    s.json().then(gosti => {
                        console.log(gosti)
                        this.tabela.dodajRed({
                            imeZgrade:this.imeZgrade,
                            /*brojLicence:licencaSelect.value,*/
                            imeGosta:gosti.korisnik.ime,
                            prezimeGosta:gosti.korisnik.prezime,
                            zaposleniLicenca:licencaSelect.value,
                            brojPasosa:pasosSelect.value,
                            brojSobe:brojSobe.value

                        })
                        brojSobe.value=null;
                    })

                }
            });
        }

        this.tabela = new Prijava(this.imeZgrade);
        this.tabela.crtaj(divZaPrijavu);
        divK.appendChild(dugmePrijavi);
        
    }

    ucitajPasose(pasosSelect)
    {
        fetch("https://localhost:5001/Korisnik/PreuzmiSveGoste/" , {
            method:"GET"
        }).then(s => {
            if (s.ok) {
                s.json().then(gost => {
                    let gostOpcija;
                    gost.forEach(gosti => {
                        gostOpcija = document.createElement("option");
                        gostOpcija.innerHTML = gosti.brojPasosa;
                        gostOpcija.value = gosti.brojPasosa;
                        pasosSelect.appendChild(gostOpcija);
                    })
                })
            }
        })
    }

    ucitajLicence(licencaSelect)
    {
        fetch("https://localhost:5001/Zaposleni/PreuzmiSveZaposlene/" , {
            method:"GET"
        }).then(s => {
            if (s.ok) {
                s.json().then(zaposlenii => {
                    let zapOpcija;
                    zaposlenii.forEach(gosti => {
                        zapOpcija = document.createElement("option");
                        zapOpcija.innerHTML = gosti.brojLicence;
                        zapOpcija.value = gosti.brojLicence;
                        licencaSelect.appendChild(zapOpcija);
                    })
                })
            }
        })
    }
}