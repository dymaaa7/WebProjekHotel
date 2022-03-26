export class Zaposleni
{
    constructor()
    {
        this.kont=null;
    }

    crtaj(kont)
    {
        this.kont=kont;
        
        fetch("https://localhost:5001/Zaposleni/PreuzmiSveZaposlene",{
            method:"GET"
    }).then(s=>{
        if(s.ok){
            s.json().then(podaci=>{
                this.crtajTabelu(podaci);
            })
        }
    })
    }

    crtajTabelu(podaci)
    {
        let divTab=document.createElement("div");
        divTab.className="okvirTabele";
        divTab.classList.add("okvirTabeleZap")
        this.kont.appendChild(divTab);
    
        let tabela = document.createElement("table");
        this.tabela=tabela;
        tabela.className="tabelaZaposleni";
        divTab.appendChild(tabela);

        let tabelaHeader=document.createElement("tr");
        tabela.appendChild(tabelaHeader);

        let tabH;
        const kolone=["Ime","Prezime","Broj licence","Broj telefona","Plata"];
        kolone.forEach(podatak=>
            {
                tabH=document.createElement("th");
                tabH.innerHTML=podatak;
                tabelaHeader.appendChild(tabH)
            })
        podaci.forEach(podatak=>{
                this.dodajRed(podatak);
            })
    }

    dodajRed(podatak){

        let tabr,tabd,dugmeUkloni,dugmeIzmeni,unos;
        tabr=document.createElement("tr");
        this.tabela.appendChild(tabr);

        tabd=document.createElement("td");
        tabd.innerHTML=podatak.imeZaposleni;
        tabr.appendChild(tabd);

        tabd=document.createElement("td");
        tabd.innerHTML=podatak.prezimeZaposleni;
        tabr.appendChild(tabd);

        tabd=document.createElement("td");
        tabd.innerHTML=podatak.brojLicence;
        tabr.appendChild(tabd);

        tabd=document.createElement("td");
        tabd.innerHTML=podatak.brTelefona;
        tabr.appendChild(tabd);

        tabd = document.createElement("td");
        tabd.innerHTML = podatak.plata;
        let plt = tabd;
        tabr.appendChild(tabd);

        tabd = document.createElement("td");
        dugmeIzmeni = document.createElement("button");
        unos = document.createElement("input");
        unos.type = "number";
        unos.placeholder = "Nova plata...";
        unos.className = "plata";
        dugmeIzmeni.className = "dugmeUkloni";
        dugmeIzmeni.innerHTML = "Izmeni";
        dugmeIzmeni.onclick = () => {
            fetch("https://localhost:5001/Zaposleni/IzmeniZaposlenog/" + podatak.brojLicence+ "/" + unos.value, {
                method: "PUT"
            }).then((s) => {
                if (s.ok) {
                    alert("Postavljena je nova plata!");
                    plt.innerHTML = unos.value;
                    unos.value = "";
                }
                else {
                    alert("Doslo je do greske!")
                }
            });
        }
        tabd.appendChild(unos)
        tabd.appendChild(dugmeIzmeni);
        tabr.appendChild(tabd);

        tabd=document.createElement("td");
        dugmeUkloni=document.createElement("button");
        dugmeUkloni.className="nazad";
        dugmeUkloni.innerHTML="Ukloni";
        dugmeUkloni.onclick=()=>
        {
            fetch("https://localhost:5001/Zaposleni/ObrisiZaposlenog/"+podatak.brojLicence,{
                method:"DELETE"
            }).then((s)=>
            {
                if(s.ok){
                    alert("Zaposleni je uklonjen");
                }
                else
                {
                    alert("Doslo je do greske.");
                }
            });
            tabr.remove();
        }
        tabd.appendChild(dugmeUkloni);
        tabr.appendChild(tabd);
    }
}