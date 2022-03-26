import {Gost} from "./Gost.js"

export class DodatiGosta
{
    constructor()
    {
        this.kont=null;
    }

    crtaj(kont)
    {
        this.kont=kont;

        let divGost=document.createElement("div");
        divGost.className="gostDiv";
        kont.appendChild(divGost);

        let divK=document.createElement("div");
        divK.className="kDiv";
        divK.classList.add("kGost");
        divGost.appendChild(divK);

        let naslovStrane=document.createElement("h2");
        naslovStrane.innerTML="Dodaj gosta";
        divK.appendChild(naslovStrane);

        let lbl=document.createElement("label");
        lbl.innerHTML="Ime gosta:";
        divK.appendChild(lbl);
        let imeGosta=document.createElement("input");
        imeGosta.type="text";
        divK.appendChild(imeGosta);

        lbl=document.createElement("label");
        lbl.innerHTML="Prezime gosta:";
        divK.appendChild(lbl);
        let prezimeGosta=document.createElement("input");
        prezimeGosta.type="text";
        divK.appendChild(prezimeGosta);

        lbl=document.createElement("label");
        lbl.innerHTML="Broj pasosa gosta:";
        divK.appendChild(lbl);
        let brojPasosa=document.createElement("input");
        brojPasosa.type="number";
        divK.appendChild(brojPasosa);

        lbl=document.createElement("label");
        lbl.innerHTML="Broj telefona gosta:";
        divK.appendChild(lbl);
        let brojTelefona=document.createElement("input");
        brojTelefona.type="number";
        divK.appendChild(brojTelefona);
        
        let dugmeDodaj=document.createElement("button");
        dugmeDodaj.innerHTML="Dodaj gosta";
        dugmeDodaj.className="nazad";
        dugmeDodaj.onclick=()=>{
            fetch("https://localhost:5001/Korisnik/DodatiKorisnika/"+imeGosta.value+"/"+prezimeGosta.value+"/"+brojPasosa.value+"/"+brojTelefona.value,{
                method:"POST"
                
            }).then((s)=>{
                if(s.ok){
                    document.querySelector(".tabelaGost").remove();
                    this.tabela=new Gost();
                    this.tabela.crtaj(divK);

                    imeGosta.value=null;
                    prezimeGosta.value=null;
                    brojPasosa.value=null;
                    brojTelefona.value=null;
                }
            })
        }

        divK.appendChild(dugmeDodaj);
        
        let tabelaGost = new Gost();
        tabelaGost.crtaj(divK);
        this.tabela = tabelaGost;
    }
}