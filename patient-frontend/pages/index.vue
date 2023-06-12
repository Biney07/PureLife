<template>
  <Header />
  <HeroSectionVue />
  <SpecializimetSectionVue />
  <FeaturesSectionVue />
  <ApplicationSectionVue />
</template>

<script>
import ApplicationSectionVue from '~/sections/ApplicationSection.vue'
import FeaturesSectionVue from '~/sections/FeaturesSection.vue'
import HeroSectionVue from '~/sections/HeroSection.vue'
import SpecializimetSectionVue from '~/sections/SpecializimetSection.vue'
import Header from "../components/Header.vue"
export default {
    components: {
        Header,
        HeroSectionVue,
        SpecializimetSectionVue,
        FeaturesSectionVue,
        ApplicationSectionVue
    }
}
</script>

<style>

</style>