import {useContext} from "react";

import {NavigationContext} from "@/components/App";
import { useAddHouse } from "@/hooks/HouseHooks";
import { House } from "@/types/house";
import ValidationSummary from "@components/ValidationSummary";
import HouseForm from "@components/house/HouseForm";
import navValues from "@/helpers/navValues";

const HouseAdd = () => {
  const { navigate } = useContext(NavigationContext);
  const addHouseMutation = useAddHouse();

  const house: House = {
    address: "",
    country: "",
    description: "",
    price: 0,
    id: 0,
    photo: "",
  };

  return (
    <>
      {addHouseMutation.isError && (
        <ValidationSummary error={addHouseMutation.error} />
      )}
      <HouseForm
        house={house}
        submitted={(house) => {
          addHouseMutation.mutate(house);
          navigate(navValues.home);
        }}
      />
    </>
  );
};

export default HouseAdd;
