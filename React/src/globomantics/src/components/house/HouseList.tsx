import React, { useContext } from "react";

import { useFetchHouses } from "@/hooks/HouseHooks";
import { House } from "@/types/house";
import ApiStatus from "@components/ApiStatus";
import currencyFormatter  from "@/helpers/currencyFormatter";
import navValues from "@/helpers/navValues";
import { NavigationContext } from "@components/App";

const HouseList = () => {
  const { navigate } = useContext(NavigationContext);
  const { data, status, isSuccess } = useFetchHouses();

  const getListQuery = useFetchHouses();
  
  console.log('isFetched, error, status, isSuccess: ', getListQuery.isFetched, getListQuery.error, getListQuery.status, getListQuery.isSuccess);
  if (!isSuccess) return <ApiStatus status={status}></ApiStatus>;

  return (
    <div>
      <div className="row mb-2">
        <h5 className="themeFontColor text-center">
          Houses currently on the market
        </h5>
      </div>
      <table className="table table-hover">
        <thead>
          <tr>
            <th>Address</th>
            <th>Country</th>
            <th>Asking Price</th>
          </tr>
        </thead>
        <tbody>
          {
            data && data.data?.map((h: House) => (
              <tr key={h.id} onClick={() => navigate(navValues.house, h.id)}>
                <td>{h.address}</td>
                <td>{h.country}</td>
                <td>{currencyFormatter.format(h.price)}</td>
              </tr>
            ))
          }
        </tbody>
      </table>
       <button className="btn btn-primary" onClick={() => navigate(navValues.houseAddNew)}>
        Add
      </button>
    </div>
  );
};

export default HouseList;