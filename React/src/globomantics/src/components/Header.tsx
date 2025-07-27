import Image from 'next/image';
import { useContext } from "react";

import GloboLogo from '@public/GloboLogo.png';
import { NavigationContext } from "@/components/App";
import navValues from "@/helpers/navValues";

type Args = {
  subtitle: string;
};

const Header = ({ subtitle }: Args) => {
  const { navigate } = useContext(NavigationContext);
  
  return (
    <header className="row mb-4">
      <div className="col-5">
        <Image src={GloboLogo} width="200" height="200" className="logo" alt="logo"
          onClick={() => {navigate(navValues.home);}} 
        />
      </div>
      <div className="col-7 mt-5 subtitle">{subtitle}</div>
    </header>
  );
};

export default Header;
