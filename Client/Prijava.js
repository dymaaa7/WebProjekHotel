export class Prijava
{
    
    constructor(imeZgrade)
    {
        this.kont=null;
        this.podaci=null;
        this.imeZgrade=imeZgrade;
        this.tabela=null;
    }


    crtaj(kont)
    {
        this.kont=kont;
        fetch("https://localhost:5001/Prijava/PrikaziPrijavuSve/"+this.imeZgrade,{
            method:"GET"
        }).then(s=>{
            if(s.ok)
            {
            s.json().then(prijave=>{
                this.podaci=prijave;
                this.crtajTabelu();
            })
        }
    })
    }

    crtajTabelu()
    {
        let divTab=document.createElement("div");
        divTab.className="okvirTabele";
        this.kont.appendChild(divTab);

        let tabela=document.createElement("table");
        this.tabela=tabela;
        tabela.className="tabelaZaposleni";
        divTab.appendChild(tabela);

        let tabelaHeader=document.createElement("tr");
        tabela.appendChild(tabelaHeader);


        let tabH;
        const podaci=["Ime gosta","Prezime gosta","Broj pasosa","Broj licence","Broj sobe","Ukloni"];
        podaci.forEach(podatak=>
            {
                tabH=document.createElement("th");
                tabH.innerHTML=podatak;
                tabelaHeader.appendChild(tabH);
            })

            this.podaci.forEach(podatak=>{
                this.dodajRed(podatak);
            })
    }

    dodajRed(podatak)
    {
        let tabr,tabd,dugmeUkloni;

        tabr=document.createElement("tr");
        this.tabela.appendChild(tabr);

        tabd=document.createElement("td");
        tabd.innerHTML=podatak.imeGosta;
        tabr.appendChild(tabd);

        tabd=document.createElement("td");
        tabd.innerHTML=podatak.prezimeGosta;
        tabr.appendChild(tabd);

        tabd=document.createElement("td");
        tabd.innerHTML=podatak.brojPasosa;
        tabr.appendChild(tabd);

        
        tabd=document.createElement("td");
        tabd.innerHTML=podatak.zaposleniLicenca;
        tabr.appendChild(tabd);

        tabd=document.createElement("td");
        tabd.innerHTML=podatak.brojSobe;
        tabr.appendChild(tabd);

        tabd = document.createElement("td");
        dugmeUkloni = document.createElement("button");
        dugmeUkloni.className = "nazad";
        dugmeUkloni.innerHTML = "Ukloni";
        dugmeUkloni.onclick = () => {
            fetch("https://localhost:5001/Prijava/IzbrisiPrijavu/" + podatak.brojSobe, {
                method: "DELETE"
            }).then((s) => {
                if (s.ok) {
                    alert("Prijava je uklonjena!");
                }
                else {
                    alert("Neuspesno brisanje prijave!")
                }
            });
            tabr.remove();
        }
        tabd.appendChild(dugmeUkloni)
        tabr.appendChild(tabd);

    }
}