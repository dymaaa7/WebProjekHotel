import {Prijava} from "./Prijava.js"
import { Zaposleni } from "./Zaposleni.js";

export class DodatiZaposlenog
{
    constructor()
    {
        this.kont=null;
    }

    crtaj(kont)
    {
        this.kont=kont;

        let divZap=document.createElement("div");
        divZap.className="zaposleniDiv";
        kont.appendChild(divZap);

        let divK=document.createElement("div");
        divK.className="kDiv";
        divK.classList.add("kZaposleni");
        divZap.appendChild(divK);

        let naslovStrane=document.createElement("h2");
        naslovStrane.innerTML="Dodaj zaposlenog";
        divK.appendChild(naslovStrane);

        let lbl=document.createElement("label");
        lbl.innerHTML="Ime zaposlenog:";
        divK.appendChild(lbl);
        let imeZaposlenog=document.createElement("input");
        imeZaposlenog.type="text";
        divK.appendChild(imeZaposlenog);

        lbl=document.createElement("label");
        lbl.innerHTML="Prezime zaposlenog:";
        divK.appendChild(lbl);
        let prezimeZaposlenog=document.createElement("input");
        prezimeZaposlenog.type="text";
        divK.appendChild(prezimeZaposlenog);

        lbl=document.createElement("label");
        lbl.innerHTML="Broj licence zaposlenog:";
        divK.appendChild(lbl);
        let brojLicence=document.createElement("input");
        brojLicence.type="number";
        divK.appendChild(brojLicence);

        lbl=document.createElement("label");
        lbl.innerHTML="Broj telefona zaposlenog:";
        divK.appendChild(lbl);
        let brojTelefona=document.createElement("input");
        brojTelefona.type="number";
        divK.appendChild(brojTelefona);

        lbl=document.createElement("label");
        lbl.innerHTML="Plata zaposlenog:";
        divK.appendChild(lbl);
        let plata=document.createElement("input");
        plata.type="number";
        divK.appendChild(plata);
        
        let dugmeDodaj=document.createElement("button");
        dugmeDodaj.innerHTML="Dodaj zaposlenog";
        dugmeDodaj.className="nazad";
        dugmeDodaj.onclick=()=>{
            fetch("https://localhost:5001/Zaposleni/DodatiZaposlenog/"+imeZaposlenog.value+"/"+prezimeZaposlenog.value+"/"+brojLicence.value+"/"+brojTelefona.value+"/"+plata.value,{
                method:"POST"
                
            }).then((s)=>{
                if(s.ok){
                    document.querySelector(".tabelaZaposleni").remove();
                    this.tabela=new Zaposleni();
                    this.tabela.crtaj(divK);

                    imeZaposlenog.value=null;
                    prezimeZaposlenog.value=null;
                    brojLicence.value=null;
                    brojTelefona.value=null;
                    plata.value=null;
                }
            })
        }

        divK.appendChild(dugmeDodaj);
        
        let tabelaZaposleni = new Zaposleni();
        tabelaZaposleni.crtaj(divK);
        this.tabela = tabelaZaposleni;
    }
}