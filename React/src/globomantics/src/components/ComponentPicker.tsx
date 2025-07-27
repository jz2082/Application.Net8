import navValues from "@/helpers/navValues";
import HouseList from "@/components/house/HouseList";
import HouseDetail from "@/components/house/HouseDetail";
import HouseAdd from "@/components/house/HouseAdd";
import HouseEdit from "@/components/house/HouseEdit";

type Args = {
  currentNavLocation: React.ReactNode;
};

const ComponentPicker = ({ currentNavLocation }: Args) => {
  switch (currentNavLocation) {
    case navValues.home:
      return <HouseList />;
    case navValues.house:
      return <HouseDetail />;
    case navValues.houseAddNew:
      return <HouseAdd />;
    case navValues.houseEdit:
      return <HouseEdit />;
    default:
      return (
        <h3>
          No component for navigation value
          { currentNavLocation } found
        </h3>
      );
  }
};

export default ComponentPicker;