export class Gost
{
    constructor()
    {
        this.kont=null;
    }

    crtaj(kont)
    {
        this.kont=kont;
        
        fetch("https://localhost:5001/Korisnik/PreuzmiSveGoste",{
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
        this.kont.appendChild(divTab);
    
        let tabela = document.createElement("table");
        this.tabela=tabela;
        tabela.className="tabelaGost";
        divTab.appendChild(tabela);

        let tabelaHeader=document.createElement("tr");
        tabela.appendChild(tabelaHeader);

        let tabH;
        const kolone=["Ime","Prezime","Broj pasosa","Broj telefona"];
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

        let tabr,tabd,dugmeUkloni;
        tabr=document.createElement("tr");
        this.tabela.appendChild(tabr);

        tabd=document.createElement("td");
        tabd.innerHTML=podatak.ime;
        tabr.appendChild(tabd);

        tabd=document.createElement("td");
        tabd.innerHTML=podatak.prezime;
        tabr.appendChild(tabd);

        tabd=document.createElement("td");
        tabd.innerHTML=podatak.brojPasosa;
        tabr.appendChild(tabd);

        tabd=document.createElement("td");
        tabd.innerHTML=podatak.brTelefona;
        tabr.appendChild(tabd);

        tabd=document.createElement("td");
        dugmeUkloni=document.createElement("button");
        dugmeUkloni.className="nazad";
        dugmeUkloni.innerHTML="Ukloni";
        dugmeUkloni.onclick=()=>
        {   
            fetch("https://localhost:5001/Korisnik/ObrisiKorisnika/"+podatak.brojPasosa,{
                method:"DELETE"
            }).then((s)=>
            {
                if(s.ok){
                    alert("Gost je uklonjen");
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