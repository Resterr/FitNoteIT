import React, { useState } from "react";
import "./home.scss";
import backgroundPhoto from "../../utils/homeBackground.png";
import { HomeInfoGold } from "../../components/homeInfoGold";
import { HomeInfoGray } from "../../components/homeInfoGray";
import { Footer } from "../../components/footer";

// import { Footer } from "../../components";

export const Home: React.FC = () => {
  const [textLeft, setTextLeft] = useState<string>(
    "Notowanie rekordów umożliwia śledzenie postępów i porównanie wyników z poprzednimi treningami. Dzięki temu użytkownicy mogą zobaczyć, jak wiele już osiągnęli i co jeszcze pozostało do osiągnięcia."
  );
  const [textCenter, setTextCenter] = useState<string>(
    "Śledzenie postępów treningowych pozwala na lepsze poznanie swojego ciała i jego reakcji na ćwiczenia. Można zobaczyć, które ćwiczenia działają najlepiej, a które należy poprawić lub zmienić."
  );
  const [textRight, setTextRight] = useState<string>(
    ' "Trening siłowy to nie tylko fizyczne ćwiczenie, ale także trening dla umysłu, który uczy nas siły woli, dyscypliny i wytrwałości." - Arnold Schwarzenegger'
  );

  return (
    <div className="home">
      <img className="home__img" src={backgroundPhoto} alt="bodybuilder" />
      <div className="home__container">
        <HomeInfoGray
          title="NOTUJ SWOJE REKORDY"
          text={textLeft}
          buttonText="TWOJE REKORDY"
        />
        <HomeInfoGray
          title="ANALIZUJ SWOJE TRENINGI"
          text={textCenter}
          buttonText="TWOJE TRENINGI"
        />
        <HomeInfoGold text={textRight} />
      </div>

      <Footer />
    </div>
  );
};
