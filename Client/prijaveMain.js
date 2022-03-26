
import { Zgrada } from "./Zgrada.js";

import {DodatiZaposlenog} from "./DodatiZaposlenog.js"



fetch("https://localhost:5001/Zgrada/PrikaziZgrade/", {
            method:"GET"
        }).then(s => {
            if (s.ok) {
                s.json().then(zgrade => {
                    zgrade.forEach(zgrada => {
                        let zgrrr = new Zgrada(zgrada.imeZgrade, zgrada.grad);
                        zgrrr.crtaj(document.body);
                    })

                })
            }
        })
       