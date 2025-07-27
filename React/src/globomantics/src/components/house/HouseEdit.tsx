import {useContext} from "react";

import {useFetchHouse, useUpdateHouse} from "@/hooks/HouseHooks";
import ValidationSummary from "@components/ValidationSummary";
import ApiStatus from "@components/ApiStatus";
import HouseForm from "@components/house/HouseForm";
import {NavigationContext} from "@/components/App";
import navValues from "@/helpers/navValues";

const HouseEdit = () => {
  const { navigate, param: id } = useContext(NavigationContext);
  const houseId = parseInt(id);
  const { data, status, isSuccess } = useFetchHouse(houseId);
  const updateHouseMutation = useUpdateHouse();

  if (!isSuccess) return <ApiStatus status={status} />;

  return (
    <>
      {updateHouseMutation.isError && (
        <ValidationSummary error={updateHouseMutation.error} />
      )}
      <HouseForm
        house={data.data}
        submitted={(house) => {
          updateHouseMutation.mutate(house);
          navigate(navValues.house, data.data?.id);
        }}
      />
    </>
  );
};

export default HouseEdit;
