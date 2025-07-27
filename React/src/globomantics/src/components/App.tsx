import React, { useCallback, useState } from "react";

import navValues from "@/helpers/navValues";
import Header from "@components/Header";

import ComponentPicker from "@components/ComponentPicker";

const NavigationContext = React.createContext({
  navState: { current: navValues.home },
});

const App = () => {
  const navigate = useCallback(
    (navTo: string, param: number) => setNav({ current: navTo, param, navigate }),
    []
  );

  const [nav, setNav] = useState({
    current: navValues.home,
    navigate
  });

  return (
    <NavigationContext.Provider value = {nav}>
      <Header subtitle="Providing houses all over the world" />
      <ComponentPicker currentNavLocation={nav.current} />
    </NavigationContext.Provider>
  );
}

export { NavigationContext };
export default App;
