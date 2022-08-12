import { shallowMount } from "@vue/test-utils";
import AdminButton from "@/components/AdminButton.vue";

describe("SolarButton.vue", () => {
  it("displays text in default slot position", () => {
    const wrapper = shallowMount(AdminButton, {
      propsData: {},
      slots: {
        default: "click here!"
      }
    });
    expect(wrapper.find("button").text()).toBe("click here!");
  });

  it("has underlying disabled button when disabled: true passed as prop", () => {
   // @ts-ignore`
    const wrapper = shallowMount(AdminButton, {
      propsData: {
        disabled: true
      },
      slots: {
        default: "foo"
      }
    });
    expect(wrapper.find("input:disabled"));
  });

  it("has no disabled on button when disabled: false passed as prop", () => {
    // @ts-ignore`
    const wrapper = shallowMount(AdminButton, {
      propsData: {
        disabled: false
      },
      slots: {
        default: "foo"
      }
    });
    expect(!wrapper.find("input:disabled"));
  });
});